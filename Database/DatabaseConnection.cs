using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SmartReader.Database
{
    public class Connection
    {
        MySqlConnection Conn;
        public Connection(string user, string password, string server = "localhost")
        {
            string connectStr = "Database=smart_reader;Data Source={0};User Id={1};Password={2}";
            Conn = new MySqlConnection(
               connectionString: String.Format(connectStr, server, user, password));
            Conn.Open();
        }

        public void NonQuery(string query)
        {
            MySqlCommand command = new MySqlCommand(query, Conn);
            command.ExecuteNonQuery();
        }
    }
}
