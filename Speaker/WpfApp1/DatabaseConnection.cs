using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    class DatabaseConnection
    {
        
            private MySqlConnection connection;
            private string server;
            private string database;
            private string username;
            private string password;

            //Constructor
            public DatabaseConnection()
            {
                Initialize();
            }

            //Initialize values
            private void Initialize()
            {
                server = "";
                database = "";
                username = "";
                password = "";
                string connectionString;
                connectionString = "server=" + server + ";" + "database=" + database + ";" + "username=" + username + ";" + "password=" + password + ";SSLMode=none";


            connection = new MySqlConnection(connectionString);
            }
        private bool Connect()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        private bool Disconnect()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("error while disconnecting");
                return false;
            }
        }
        public async void InsertID(string id, string name)
        {
            if(this.Connect() == true)
            {
                var query = "INSERT INTO `Profiles` (`ProfileId`, `Name`) VALUES ('"+id+"', '"+name+"');";
                MySqlCommand cmd = new MySqlCommand(query,this.connection);
                await cmd.ExecuteNonQueryAsync();
                this.Disconnect();
            }
            
        }
        public string GetName(string id)
        {
            if (this.Connect() == true)
            {
                var query = " SELECT `name` FROM `Profiles` WHERE `ProfileId` = '"+id+"';";
                MySqlCommand cmd = new MySqlCommand(query, this.connection);
                cmd.ExecuteNonQueryAsync();
                try
                {
                    string returnname = "";
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        returnname = dataReader.GetString(0);
                        
                    }
                    dataReader.Close();
                    this.Disconnect();
                    return returnname;
                }
                catch(MySqlException ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "error";
                }
                
            }
            else
            {
                return "Name not found";
            }
        }

    }
}
