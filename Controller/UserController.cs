using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketGameProtocol;
using SocketGameServer.Servers;

namespace SocketGameServer.Controller
{
    class UserController : BaseController
    {
        public UserController()
        {
            requestCode = RequestCode.User;
        }

        public MainPack Logon(Server server,Client client,MainPack pack)
        {
            if(server.Logon(client, pack))
            {
                pack.Returncode = ReturnCode.Succeed;
            }
            else
            {
                pack.Returncode = ReturnCode.Fail;
            }
            return pack;
        }

        public MainPack Login(Server server, Client client, MainPack pack)
        {

        }
    }
}
