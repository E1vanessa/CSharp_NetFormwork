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
using MySql.Data.MySqlClient;

namespace SocketGameServer.Servers
{
    class Client
    {
        private string connstr = "database=shipgame_userinfo;user=root;data source=127.0.0.1;password=510704;pooling=false;charset=utf8;port=3306";

        private Socket socket;
        private Message message;
        private UserData userData;
        private Server server;
        private MySqlConnection sqlConnection;

        public UserData GetUserData { get { return userData; } }
        public Client(Socket socket,Server server)
        {
            
            userData = new UserData();
            message = new Message();
            sqlConnection = new MySqlConnection(connstr);
            sqlConnection.Open();
            this.server = server;
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

                Console.WriteLine("已接收到请求");
                message.ReadBuffer(len,HandleRequest);
                StartReceive();
            }
            catch
            {

                
            }
        }

        public void Send(MainPack pack)
        {
            socket.Send(Message.PackData(pack));
            Console.WriteLine("反馈成功");
        }

        private void HandleRequest(MainPack pack)
        {
            server.HandleRequest(pack, this);
        }

        public bool Logon(MainPack pack)
        {
            return GetUserData.Logon(pack,sqlConnection);
        }
    }
}
