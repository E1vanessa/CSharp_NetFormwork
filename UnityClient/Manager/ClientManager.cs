using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using SocketGameProtocol;

public class ClientManager : BaseManager
{
    
    public ClientManager(GameFace face) : base(face) { }

    private Socket socket;
    private Message message;

    public override void OnInit()
    {
        base.OnInit();
        message = new Message();
        InitSocket();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        message = null;
        CloseSocket();
    }

    private void InitSocket()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            socket.Connect("127.0.0.1", 6666);
            StartReceive();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private void CloseSocket()
    {
        if (socket.Connected && socket != null)
        {
            socket.Close();
        }
    }

    private void StartReceive()
    {
        socket.BeginReceive(message.Buffer,message.startIndex,message.Remsize,SocketFlags.None,ReceiveCallback,null);
    }

    private void ReceiveCallback(IAsyncResult iar)
    {
        try
        {
            if (socket == null || socket.Connected == false) return;
            int len = socket.EndReceive(iar);
            if(len == 0)
            {
                CloseSocket();
                return;
            }
            message.ReadBuffer(len,HandleResponse);
            StartReceive();
        }
        catch 
        {

        }
    }

    private void HandleResponse(MainPack pack)
    {
        face.HandleResponse(pack);
    }

    public void Send(MainPack pack)
    {
        socket.Send(Message.PackData(pack));
    }


}
