using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using SocketGameServer.Controller;
using MySql.Data.MySqlClient;
using SocketGameProtocol;

namespace SocketGameServer.Servers
{
    class Server
    {
        private Socket socket;
        private List<Client> clientList = new List<Client>();
        private ControllerManager controllerManager;
        public Server(int port)
        {
            controllerManager = new ControllerManager(this);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            socket.Listen(0);
            StartAccept();
            Console.WriteLine("服务端已开启");
        }

        void StartAccept()
        {
            socket.BeginAccept(AcceptCallback,null);
        }

        void AcceptCallback(IAsyncResult iar) 
        {
            Socket client = socket.EndAccept(iar);
            clientList.Add(new Client(client,this));
            StartAccept();
        }

        public void HandleRequest(MainPack pack,Client client)
        {
            controllerManager.HandleRequest(pack, client);
        }
    }
}
