using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using S = ServerPackets;
using UnityEngine;

public abstract class Packet
{
    public abstract short Index { get; }

    public static Packet ReceivePacket(byte[] rawBytes, out byte[] extra)
    {
        extra = rawBytes;
        Packet p;
        //| 2Bytes: Packet Size | 2Bytes: Packet ID |
        if (rawBytes.Length < 4) return null;
        int length = (rawBytes[1] << 8) + rawBytes[0];
        if (rawBytes.Length < length || length < 2) return null;
        MemoryStream stream = new MemoryStream(rawBytes, 2, length - 2);
        BinaryReader reader = new BinaryReader(stream);
        try
        {
            short id = reader.ReadInt16();
            p = GetServerPacket(id);
            if (p != null)
            {
                //调用实现
                p.ReadPacket(reader);
            }else{
                Logger.Errorf("ReceivePacket invalid packet id:%d", id);
            }
        }
        catch (System.Exception e)
        {
            Logger.Errorf("receivePacket exception:%s", e.ToString());
            Debug.Log(e);
            return null;
        }
        // 剩余的数据，返回回去，下一次拆包处理
        extra = new byte[rawBytes.Length - length];
        Buffer.BlockCopy(rawBytes, length, extra, 0, rawBytes.Length - length);
        return p;
    }
    public IEnumerable<byte> GetPacketBytes()
    {
        if (Index < 0) return new byte[0];

        byte[] data;

        using (MemoryStream stream = new MemoryStream())
        {
            stream.SetLength(2);
            stream.Seek(2, SeekOrigin.Begin);
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(Index);
                WritePacket(writer);
                stream.Seek(0, SeekOrigin.Begin);
                writer.Write((short)stream.Length);
                stream.Seek(0, SeekOrigin.Begin);

                data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
            }
        }

        return data;
    }
    protected abstract void ReadPacket(BinaryReader reader);
    protected abstract void WritePacket(BinaryWriter writer);

    private static Packet GetServerPacket(short index)
    {
        switch (index)
        {
            case (short)ServerPacketIds.KeepAlive:
                return new S.KeepAlive();
            case (short)ServerPacketIds.Connected:
                return new S.Connected();
            case (short)ServerPacketIds.ClientVersion:
                return new S.ClientVersion();
            case (short)ServerPacketIds.Disconnect:
                return new S.Disconnect();
            case (short)ServerPacketIds.NewAccount:
                return new S.NewAccount();
            case (short)ServerPacketIds.ChangePassword:
                return new S.ChangePassword();
            case (short)ServerPacketIds.ChangePasswordBanned:
                return new S.ChangePasswordBanned();
            case (short)ServerPacketIds.Login:
                return new S.Login();
            case (short)ServerPacketIds.LoginSuccess:
                return new S.LoginSuccess();
            default:
                return null;
        }
    }
}
