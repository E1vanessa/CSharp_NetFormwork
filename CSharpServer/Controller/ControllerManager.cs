using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketGameProtocol;
using System.Reflection;
using SocketGameServer.Servers;

namespace SocketGameServer.Controller
{
    class ControllerManager
    {
        protected Dictionary<RequestCode, BaseController> controlDict = new Dictionary<RequestCode, BaseController>();

        private Server server;

        public ControllerManager(Server server)
        {
            this.server = server;
            UserController userController = new UserController();
            controlDict.Add(userController.GetRequestCode,userController);
        }

        public void HandleRequest(MainPack pack,Client client)
        {
            if (controlDict.TryGetValue(pack.Requestcode, out BaseController controller))
            {
                string metname = pack.Actioncode.ToString();
                MethodInfo method = controller.GetType().GetMethod(metname);
                if(method == null)
                {
                    Console.WriteLine("没有找到指定的事件处理"+pack.Actioncode.ToString());
                    return;
                }
                object[] obj = new object[] {client,pack };
                object ret = method.Invoke(controller,obj);
                if(ret != null)
                {
                    client.Send(ret as MainPack);
                }
            }
            else
            {
                Console.WriteLine("没有找到对应的controller处理");
            }
        }
    }
}
