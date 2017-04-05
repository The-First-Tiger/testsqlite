using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_SqLite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            if (File.Exists("TestDb.sqlite"))
            {
                Console.WriteLine("Löschen der alten DB");
                File.Delete("TestDb.sqlite");
            }

            Console.WriteLine("Creating DB");
            SQLiteConnection.CreateFile("TestDb.sqlite");

            Console.WriteLine("Connection to DB");
            var connection = new SQLiteConnection("Data Source=TestDb.sqlite;Version=3");
            connection.Open();

            Console.WriteLine("Creating table test");
            var command = new SQLiteCommand("create table test (value nvarchar(255))", connection);
            command.ExecuteNonQuery();

            Console.WriteLine("Insert value into table test");
            command = new SQLiteCommand("insert into test (value) values ('hello sqlite')", connection);
            command.ExecuteNonQuery();

            Console.WriteLine("Selecting value from table test");
            command = new SQLiteCommand("select * from test", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("Value: " + reader.GetString(0));
                }
            }

            connection.Close();

            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
