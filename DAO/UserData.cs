using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketGameProtocol;

namespace SocketGameServer.DAO
{
    class UserData
    {
        private MySqlConnection mysqlCon;

        private string connstr = "database=sys;data source=127.0.0.1;password=root;pooling=false;charset=utf8;port=3306";

        public UserData()
        {
            ConnectMysql();
        }

        private void ConnectMysql()
        {
            try
            {
                mysqlCon = new MySqlConnection(connstr);
                mysqlCon.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine("连接数据库失败!");
            }
        }

        public bool Logon(MainPack pack)
        {
            string username = pack.Loginpack.Username;
            string password = pack.Loginpack.Password;

            string sql = "SELECT * shipgame_userinfo.userdata where username='@username'";
            MySqlCommand comd = new MySqlCommand(sql,mysqlCon);
            MySqlDataReader read = comd.ExecuteReader();
            if (read.Read())
            {
                //用户名被注册;
                return false;
            }
            //插入数据
            sql = "INSERT INTO shipgame_userinfo.userdata (username,password) VAIUES ( '@username','@password')";
            comd = new MySqlCommand(sql,mysqlCon);
            try
            {
                comd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
            
        }
    }
}
