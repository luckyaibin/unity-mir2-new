
using System.Collections.Concurrent;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
public static class Network
{
    public static bool Connected;
    public static IProcessPacket loginScens;
    public static IProcessPacket gameScens;

    private static TcpClient _client;
    private static string _host;
    private static int _port;

    // 连接成功时间戳
    private static DateTime ConnectedAt;
    private static DateTime LastKeepAliveSendAt;

    // 经过多久没有回包就断开连接
    private static TimeSpan HeartBeatSpan = TimeSpan.FromSeconds(5);

    private static ConcurrentQueue<Packet> _receiveList;
    private static ConcurrentQueue<Packet> _sendList;
    private static byte[] _rawData = new byte[0];

    private static readonly byte[] _rawBytes = new byte[8 * 1024];
    public static void Connect(string host, int port)
    {
        if (_client != null)
        {
            Disconnect();
        }
        _host = host;
        _port = port;
        _client = new TcpClient();
        _client.NoDelay = true;
        _client.BeginConnect(host, port, ConnectionCB, null);
    }
    private static void ConnectionCB(IAsyncResult result)
    {
        try
        {
            _client.EndConnect(result);
            if (!_client.Connected)
            {
                // 重连
                Connect(_host, _port);
                return;
            }
            _receiveList = new ConcurrentQueue<Packet>();
            _sendList = new ConcurrentQueue<Packet>();
            _rawData = new byte[0];

            // 开始接收数据
            BeginReceive();
        }
        catch (SocketException ex)
        {

        }
    }
    public static void Disconnect()
    {
        Connected = false;
        if (_client == null) return;
        _client.Close();
        _sendList = null;
        _receiveList = null;
        _client = null;

    }
    private static void BeginReceive()
    {
        if (_client == null || !_client.Connected) return;
        try
        {
            _client.Client.BeginReceive(_rawBytes, 0, _rawBytes.Length, SocketFlags.None, Network.ReceiveData, _rawBytes);
        }
        catch (Exception ex)
        {
            Logger.Errorf("network error %s", ex.ToString());
            Network.Disconnect();
        }

    }
    private static void ReceiveData(IAsyncResult result)
    {
        if (_client == null || !_client.Connected) return;
        int dataRead;

        try
        {
            dataRead = _client.Client.EndReceive(result);
        }
        catch (Exception ex)
        {
            Logger.Errorf("%s" + ex.ToString());
            Disconnect();
            return;
        }
        if (dataRead == 0)
        {
            Logger.Warnf("disconnected ...");
            Disconnect();
        }
        byte[] rawBytes = result.AsyncState as byte[];
        byte[] temp = _rawData;
        // 把新接收到的数据放到后面
        _rawData = new byte[dataRead + temp.Length];
        Buffer.BlockCopy(temp, 0, _rawData, 0, temp.Length);
        Buffer.BlockCopy(rawBytes, 0, _rawData, temp.Length, dataRead);

        Packet p;
        while ((p = Packet.ReceivePacket(_rawData, out _rawData)) != null)
        {
            _receiveList.Enqueue(p);
        }
        // 继续下一次收包
        Network.BeginReceive();
    }

    private static void BeginSend(List<byte> data)
    {
        if (_client == null || !_client.Connected || data.Count == 0) return;
        try
        {
            _client.Client.BeginSend(data.ToArray(), 0, data.Count, SocketFlags.None, Network.SendData, null);
        }
        catch (Exception ex)
        {
            Logger.Errorf("发送数据失败:%s", ex.ToString());
            Disconnect();
        }
    }
    private static void SendData(IAsyncResult result)
    {
        try
        {
            _client.Client.EndSend(result);
        }
        catch (Exception ex)
        {
            Logger.Errorf("发送数据 EndSend 失败:%s", ex.ToString());
            Disconnect();
        }
    }

    public static void Tick()
    {
        // 连接已经断开,把所有包处理完然后关闭连接
        if (_client == null || !_client.Connected)
        {
            if (!Connected)
            {
                return;
            }
            while (_receiveList != null && !_receiveList.IsEmpty)
            {
                if (!_receiveList.TryDequeue(out Packet p) || p == null) continue;
                if (p is not ServerPackets.Disconnect && p is not ServerPackets.ClientVersion) continue;
                DispatchPacket(p);
                _receiveList = null;
                return;
            }
            Logger.Errorf("Lost connection with the server.");
            Disconnect();
            return;
        }
        // tcp已经连接好,但是一直没有回包(回包时候设置Connected为true),
        if (!Connected && ConnectedAt > DateTime.MinValue && DateTime.Now > ConnectedAt + HeartBeatSpan)
        {
            Disconnect();
            Connect(_host, _port);
            return;
        }

        // 1. 收包-正常处理数据包
        while (_receiveList != null && !_receiveList.IsEmpty)
        {
            if (!_receiveList.TryDequeue(out Packet p) || p == null) continue;
            DispatchPacket(p);
        }

        // 2. 发包
        // 需要发送心跳(仅当发送队列为空)
        if (DateTime.Now > LastKeepAliveSendAt + HeartBeatSpan && _sendList != null && _sendList.IsEmpty)
        {
            _sendList.Enqueue(new ServerPackets.KeepAlive());
        }
        if (_sendList == null || _sendList.IsEmpty)
        {
            return;
        }

        // packet 转数组
        List<byte> data = new List<byte>();
        while (!_sendList.IsEmpty)
        {
            if (!_sendList.TryDequeue(out Packet p)) continue;
            data.AddRange(p.GetPacketBytes());
        }
        LastKeepAliveSendAt = DateTime.Now;

        BeginSend(data);
    }

    public static void Enqueue(Packet p)
    {
        if (_sendList != null && p != null)
            _sendList.Enqueue(p);
    }

    private static void DispatchPacket(Packet p)
    {
        Logger.Debugf("网络收到包: %s", p.ToString());
        if (loginScens != null)
            loginScens.process(p);
        if (gameScens != null)
            gameScens.process(p);
        if (loginScens==null && gameScens==null){
            Logger.Errorf("none net message processed ...");
        }
    }
}
public interface IProcessPacket
{
    void process(Packet p);
}