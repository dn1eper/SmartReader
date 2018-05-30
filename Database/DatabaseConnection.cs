using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SmartReader.Database
{
    public partial class DatabaseConnection
    {
        private MySqlConnection Conn;
        public DatabaseConnection(string user, string password, string server = "localhost")
        {
            //string connectStr = "Database=smart_reader;Data Source={0};User Id={1};Password={2};CHARSET=utf8";
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = server;
            conn_string.UserID = user;
            conn_string.Password = password;
            conn_string.Database = "smart_reader";
            conn_string.CharacterSet = "utf8";
            //conn = new MySqlConnection(conn_string.ToString());
            //Conn = new MySqlConnection(
            //   connectionString: String.Format(connectStr, server, user, password));
            Conn = new MySqlConnection(conn_string.ToString());
            Conn.Open();
        }

        public void NonQuery(string query)
        {
            MySqlCommand command = new MySqlCommand(query, Conn);
            
            command.ExecuteNonQuery();
        }

    }
}
