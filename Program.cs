using System.Collections.Generic;
using System.Linq;
using SocketGameServer.Servers;
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(6666);
            Console.Read();
        }
    }