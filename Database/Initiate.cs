using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Database
{
    static class Initiate
    {
        /// <summary>
        /// Вызывает исключение, если приконнектиться к базе невозможно.
        /// Создаёт базу данных и таблицы с помощью переданного подключения.
        /// </summary>

        const string DATABASE_NAME = "smart_reader";
        public static void PrepareDatabase(string login, string password)
        {
            MySqlConnection mysql = new MySqlConnection("server=localhost; user id=root; password=;");
            mysql.Open();
            try
            {
                MySqlCommand command = mysql.CreateCommand();
                command.CommandText = "CREATE DATABASE @DatabaseName; USE @DatabaseName";
                command.Parameters.AddWithValue("DatabaseName", DATABASE_NAME);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (mysql.State == ConnectionState.Open)
                {
                    mysql.Close();
                }
            }
        }
    }
}
