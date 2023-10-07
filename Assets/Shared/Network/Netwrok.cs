
using System.Collections.Concurrent;
using System.Net.Sockets;
using System;
public static class Network {
    private static TcpClient _client;
    private static string _host;
    private static int _port;
    private static ConcurrentQueue<Packet> _receiveList;
    private static ConcurrentQueue<Packet> _sendList;

    public static void Connect(string host,int port){
        if(_client!=null){
            Disconnect();
        }
        _host = host;
        _port = port;
        _client = new TcpClient();
        _client.NoDelay = true;
        _client.BeginConnect(host,port,ConnectionCB,null);
    }
    private static void ConnectionCB(IAsyncResult result){
        try{
            _client.EndConnect(result);
            if(!_client.Connected){
                // 重连
                Connect(_host,_port);
                return;
            }
            _receiveList = new ConcurrentQueue<Packet>();
            _sendList = new ConcurrentQueue<Packet>();
        }catch(SocketException ex){
            
        }
    }
    public static void Disconnect(){
        if(_client==null) return;
        _client.Close();
        _sendList = null;
        _receiveList = null;
        _client = null;
    }
}