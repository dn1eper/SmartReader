using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Database
{
    public partial class DatabaseConnection
    {
        private const string InsertUserQuery = "INSERT INTO person (login, pass_hash) VALUES (@Login, @PassHash)";
        private const string GetPersonPassByLoginQuery = "SELECT pass_hash FROM person WHERE login = @Login";
        private const string InsertTokenQuery = "INSERT INTO token (login, token) VALUES (@Login, @Token)";
        private const string GetTokenForLoginQuery = "SELECT token FROM token WHERE login = @Login";
        private const string GetLoginForTokenQuery = "SELECT login FROM token WHERE token = @Token";
        public void InsertUser(string login, string hashedPassword)
        {
            
            MySqlCommand cmd = new MySqlCommand(InsertUserQuery, Conn);
            cmd.Parameters.AddWithValue("@Login", login);
            cmd.Parameters.AddWithValue("@PassHash", hashedPassword);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception)
            {
                throw new Exception(message: "Пользователь с таким логином существует.");
            }
        }

        public string GetPersonPassHash(string login)
        {
            MySqlCommand cmd = new MySqlCommand(GetPersonPassByLoginQuery, Conn);
            cmd.Parameters.AddWithValue("@Login", login);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if(reader.Read())
                {
                    return reader.GetString(0);
                }
                else
                {
                    throw new Exception(message: "Нет такого пользователя.");
                }
            }
        }

        public void InsertToken(string login, string token)
        {
            MySqlCommand cmd = new MySqlCommand(InsertTokenQuery, Conn);
            cmd.Parameters.AddWithValue("@Login", login);
            cmd.Parameters.AddWithValue("@Token", token);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception(message: "У пользователя уже имеется токен.");
            }
        }

        public string GetTokenFor(string login)
        {
            MySqlCommand cmd = new MySqlCommand(GetTokenForLoginQuery, Conn);
            cmd.Parameters.AddWithValue("@Login", login);
            return GetOneStringValue(cmd);
        }

        public string GetLoginFor(string token)
        {
            MySqlCommand cmd = new MySqlCommand(GetLoginForTokenQuery, Conn);
            cmd.Parameters.AddWithValue("@Token", token);
            return GetOneStringValue(cmd);
        }

        private string GetOneStringValue(MySqlCommand cmd)
        {
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
