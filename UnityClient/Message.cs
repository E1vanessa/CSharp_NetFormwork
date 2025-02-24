using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SocketGameProtocol;
using Google.Protobuf;

public class Message
{
    private byte[] buffer = new byte[1024];
    private int startindex;
    public byte[] Buffer
    {
        get { return buffer; }
    }

    public int startIndex
    {
        get { return startindex; }
    }

    public int Remsize
    {
        get { return buffer.Length - startindex; }
    }

    public void ReadBuffer(int len, Action<MainPack> HandleResponse)
    {
        startindex += len;
        if (startindex <= 4) return;
        int count = BitConverter.ToInt32(buffer, 0);
        while (true)
        {
            if (startindex >= (count + 4))
            {
                MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 4, count);
                HandleResponse(pack);
                Array.Copy(buffer, count + 4, buffer, 0, startIndex - count - 4);
                startindex -= (count + 4);
            }
            else
            {
                break;
            }
        }
    }

    public static byte[] PackData(MainPack pack)
    {
        byte[] data = pack.ToByteArray();
        byte[] head = BitConverter.GetBytes(data.Length);
        return head.Concat(data).ToArray();
    }
}
