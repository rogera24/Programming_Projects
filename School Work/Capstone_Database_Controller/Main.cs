using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

//This Main function should not be run on an active database, it will cause inconsistencies in the data.
//These functions are examples on how to run the Database Controller Class

namespace Database_Controller
{
    class Database_Control
    {
        static void Main(string[] args)
        {
            Database_Controller Session = new Database_Controller();
            MySqlConnection connection = Session.Input_Form();

            Boolean create = Session.Create_Usr(connection, "Bob", "rab313222");
            Boolean create2 = Session.Create_Usr(connection, "ablivion", "rab313222");

            Console.Out.WriteLine(create);
            Console.Out.WriteLine(create2);

            int log1 = Session.Login(connection, "ablivion", "rab313222");

            int log2 = Session.Login(connection, "bob", "rab313222");

            int log3 = Session.Login(connection, "roger", "lolololrab313222");

            Console.Out.WriteLine(log1);
            Console.Out.WriteLine(log2);
            Console.Out.WriteLine(log3);

            Console.Out.Write("Say something cool: ");
            string say = Console.In.ReadLine();

            Session.insert_chat(connection, "ablivion", say, 717);

            List<string> list = Session.chatroom_recall(connection, 717);

            foreach (string s in list)
            {
                Console.Out.WriteLine(s);
            }

            List<string> go = Session.playerchat_recall(connection, "ablivion");

            foreach (string s in list)
            {
                Console.Out.WriteLine(s);
            }

            string [] users = {"ablivion", "rogera24", "bob", "robert", "joejoe", "steve"};

            int game_id = Session.create_game(connection, users, "blahgah");
            Console.Out.WriteLine(game_id);

            int message_id = Session.message_save(connection, "This is not a drill! Woop Woop!", 12);
            Console.Out.WriteLine(message_id);

            connection.Close();

        }
    }
}
