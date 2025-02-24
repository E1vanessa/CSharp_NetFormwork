using MySql.Data.MySqlClient;
using SocketGameProtocol;

namespace SocketGameServer.DAO
{
    class UserData
    {
        public bool Logon(MainPack pack,MySqlConnection sqlConnection)
        {
            string username = pack.Loginpack.Username;
            string password = pack.Loginpack.Password;

            string selectSql = "SELECT * FROM shipgame_userinfo.userdata WHERE username = @username";
            using (MySqlCommand selectCommand = new MySqlCommand(selectSql, sqlConnection))
            {
                selectCommand.Parameters.AddWithValue("@username", username);
                using (MySqlDataReader reader = selectCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // 用户名已被注册
                        return false;
                    }
                }
            }

            // 插入数据
            string insertSql = "INSERT INTO shipgame_userinfo.userdata (username, password) VALUES (@username, @password)";
            try
            {
                using (MySqlCommand insertCommand = new MySqlCommand(insertSql, sqlConnection))
                {
                    insertCommand.Parameters.AddWithValue("@username", username);
                    insertCommand.Parameters.AddWithValue("@password", password);
                    insertCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                // 处理 MySQL 相关异常
                Console.WriteLine($"MySQL 异常: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // 处理其他异常
                Console.WriteLine($"其他异常: {ex.Message}");
                return false;
            }
        }
    }
}
