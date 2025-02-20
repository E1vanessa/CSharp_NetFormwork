using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using SocketGameServer.Tool;
using SocketGameServer.Servers;
using SocketGameServer.DAO;
using SocketGameProtocol;

namespace SocketGameServer.Servers
{
    class Client
    {
        private Socket socket;
        private Message message;
        private UserData userData;

        public UserData GetUserData { get { return userData; } }
        public Client(Socket socket)
        {
            userData = new UserData();
            message = new Message();

            this.socket = socket;
            message = new Message();
            StartReceive();
        }

        void StartReceive()
        {
            socket.BeginReceive(message.Buffer,message.startIndex,message.Remsize,SocketFlags.None,ReceiveCallback,null);
        }

        void ReceiveCallback(IAsyncResult iar)
        {
            try
            {
                if (socket == null || socket.Connected == false) return;
                int len = socket.EndReceive(iar);
                if (len == 0)
                {
                    return;
                }

                message.ReadBuffer(len);
                StartReceive();
            }
            catch
            {

                
            }
        }

        public void Send(MainPack pack)
        {
            socket.Send(Message.PackData(pack));
        }
    }
}
