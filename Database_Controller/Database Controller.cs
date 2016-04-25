using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

//When using any function below Connect and Input_Form, make sure to open your conenction before hand and close the connection after each function

namespace Database_Controller
{
    public class Database_Controller
    {
        public MySqlConnection Input_Form()
        {
            Console.Out.Write("Please enter the database location: ");
            string url = Console.In.ReadLine();
            Console.Out.Write("\nPlease enter port number or hit enter to default: ");
            string port = Console.In.ReadLine();
            if (port == "")
            {
                port = "3306";
            }
            string username = "";
            while (username == "")
            {
                Console.Out.Write("\nPlease enter your database admin username: ");
                username = Console.In.ReadLine();
            }
            string password = "";
            while (password == "")
            {
                Console.Out.Write("\nPlease enter your databae admin password: ");
                password = Console.In.ReadLine();
            }
            Console.Out.WriteLine("\n\nThank you, attempting to connect to database...");
            MySqlConnection connection = Connect(url, port, username, password);
            return connection;
        }
        public MySqlConnection Connect(string url, string port, string username, string password)
        {
            string MyConnectionString = "datasource=" + url + ";port=" + port + ";username=" + username + ";password=" + password + ";";
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            try
            {
                Console.Out.WriteLine("\nAttempting to connect to specified server.");
                connection.Open();
                Console.Out.WriteLine("\nConnection Successful");
            }
            catch
            {
                Console.Out.WriteLine("I'm sorry but your connection was not successful.");
            }
            return connection;
        }
        public int Login(MySqlConnection connection, string usrname, string hashpass)
            //------------------------------------
            //Login Return Codes
            //------------------------------------
            //Error Code 0 = Correct Credentials
            //Error Code 1 = User does not exist
            //Error Code 2 = Incorrect Password
            //Error Code 3 = Connection Issue
            //------------------------------------
        {
            try
            {
                int ErrorCode = 0;
                string checkusr = "SELECT * FROM `risk`.`user_table`";
                MySqlCommand usrcheck = new MySqlCommand(checkusr, connection);
                MySqlDataReader dataReader = usrcheck.ExecuteReader();

                while (dataReader.Read())
                {
                    if (String.Equals((dataReader["user_name"] + ""), usrname, StringComparison.OrdinalIgnoreCase) == true)
                    {
                        if (String.Equals((dataReader["user_pwd"] + ""), hashpass, StringComparison.OrdinalIgnoreCase) == true)
                        {
                            Console.Out.WriteLine("User name and password correct in new form of checker.");
                            dataReader.Close();
                            ErrorCode = 0;
                            string updatelastlog = "UPDATE `risk`.`user_table` SET last_login = NOW() WHERE user_name = '"+usrname+"'";
                            MySqlCommand lastlog = new MySqlCommand(updatelastlog, connection);
                            lastlog.ExecuteNonQuery();
                            return ErrorCode;
                        }
                        else
                        {
                            Console.Out.WriteLine("Found User name, pass wrong.");
                            dataReader.Close();
                            ErrorCode = 2;
                            return ErrorCode;
                        }
                    }
                }
                dataReader.Close();
                Console.Out.WriteLine("I am sorry but the user name you attempted to use does not exist.");
                ErrorCode = 1;
                return ErrorCode;
            }
            catch
            {
                Console.Out.WriteLine("\nSorry but the action preformed was unssucessful. Check your connection.");
                int ErrorCode = 3;
                return ErrorCode;
            }
        }
        public Boolean Create_Usr(MySqlConnection connection, string usrname, string hashpass)
        {
            try
            {
                string checkusr = "SELECT * FROM `risk`.`user_table`";
                MySqlCommand usrcheck = new MySqlCommand(checkusr, connection);
                MySqlDataReader dataReader = usrcheck.ExecuteReader();
                
                while (dataReader.Read())
                {
                    if (String.Equals((dataReader["user_name"] + ""), usrname, StringComparison.OrdinalIgnoreCase) == true)
                    {
                        Console.Out.WriteLine("Username already exists.");
                        dataReader.Close();
                        return false;
                    }
                }
                dataReader.Close();

            }

            catch { Console.Out.WriteLine("\nSorry but the action preformed was unssucessful. Check your connection."); return false; }
            
            try
            {
                string usrstring = "INSERT INTO `risk`.`user_table` (`user_name`, `user_pwd`) VALUES ('" + usrname + "', '" + hashpass + "');";
                MySqlCommand usercreate = new MySqlCommand(usrstring);
                usercreate.Connection = connection;
                usercreate.ExecuteNonQuery();
                Console.Out.WriteLine("\nSuccess, User Created!");
                return true;
            }
            catch { Console.Out.Write("User Creation was unsuccessful."); return false; }
            
        }
        public Boolean insert_chat(MySqlConnection connection, string username, string log, int gameid)
        {
            try
            {
                string insert = "INSERT INTO `risk`.`chat_log` VALUES('"+gameid+"','"+username+"',NOW(),'"+log+"','')";
                MySqlCommand chatinsert = new MySqlCommand(insert);
                chatinsert.Connection = connection;
                chatinsert.ExecuteNonQuery();
                return true;
            }
            catch {return false; }
            
        }
        public List<string> chatroom_recall(MySqlConnection connection, int gameid)
        {
            MySqlDataReader dataReader;
            List<string> log = new List<string>();
            try
            {
                string callchat = "SELECT * FROM `risk`.`chat_log`";
                MySqlCommand call = new MySqlCommand(callchat, connection);
                dataReader = call.ExecuteReader();
                while (dataReader.Read())
                {
                    if (Convert.ToInt32(dataReader["chat_id"]) == gameid)
                    {
                        log.Add("Message ID: " + dataReader["message_id"] + "[" + dataReader["date_stamp"] + "], User: " + dataReader["user_id"] + ", Sent: " + dataReader["text_body"]);
                    }
                }
                dataReader.Close();
            }
            catch { throw; }
            dataReader.Close();
            return log;
        }
        public List<string> playerchat_recall(MySqlConnection connection, string username)
        {
            MySqlDataReader dataReader;
            List<string> log = new List<string>();
            try
            {
                string callchat = "SELECT * FROM `risk`.`chat_log`";
                MySqlCommand call = new MySqlCommand(callchat, connection);
                dataReader = call.ExecuteReader();
                while (dataReader.Read())
                {
                    if (String.Equals((dataReader["user_id"] + ""), username, StringComparison.OrdinalIgnoreCase) == true)
                    {
                        log.Add("Message ID: " + dataReader["message_id"] + " [" + dataReader["date_stamp"] + "]  In Game: " + dataReader["chat_id"] + username + ", Sent: " + dataReader["text_body"]);
                    }
                }
                dataReader.Close();
            }
            catch { throw; }
            dataReader.Close();
            return log;
        }
        public int create_game(MySqlConnection connection, string[] users, string game_name)
            //users List must be 6 values long, null is allowed
        {
            for (int i = 0; i < 6; i++)
            {
                if (users[i] == null) { users[i] = ""; }
                else { }
            }
            Int32 game_id = 0;
            try
            {
                string gamecr = "INSERT INTO risk.game_log VALUES('', NOW(), '', @user1, @user2, @user3, @user4, @user5, @user6, '', @name); SELECT LAST_INSERT_ID()";
                MySqlCommand creategame = new MySqlCommand(gamecr, connection);

                creategame.Parameters.AddWithValue("@user1", users[0]);
                creategame.Parameters.AddWithValue("@user2", users[1]);
                creategame.Parameters.AddWithValue("@user3", users[2]);
                creategame.Parameters.AddWithValue("@user4", users[3]);
                creategame.Parameters.AddWithValue("@user5", users[4]);
                creategame.Parameters.AddWithValue("@user6", users[5]);
                creategame.Parameters.AddWithValue("@name", game_name);

                game_id = Convert.ToInt32(creategame.ExecuteScalar());
                Console.Out.WriteLine("\nSuccess, lobby data created!");
            }
            catch
            {
                throw;
                return game_id;
            }
            return game_id;
        }
        public Boolean premature_close(MySqlConnection connection, string[] players, int gameid)
        {
            return false;
        }
        public Boolean game_finished(MySqlConnection connection)
        {
            return false;
        }
        public int message_save(MySqlConnection connection, string message, int gameid)
        {
            Int32 message_id = 0;
            try
            {
                string script = "INSERT INTO risk.action_log VALUES(@game_id, @message, ''); SELECT LAST_INSERT_ID()";
                MySqlCommand cr_message = new MySqlCommand(script, connection);

                cr_message.Parameters.AddWithValue("@game_id", gameid);
                cr_message.Parameters.AddWithValue("@message", message);

                message_id = Convert.ToInt32(cr_message.ExecuteScalar());
                Console.Out.WriteLine("Message Added");
            }
            catch
            {
                throw;
                return 0;
            }
            return message_id;
        }
    }
}
