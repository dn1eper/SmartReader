using MySql.Data.MySqlClient;
using SmartReader.Library.Book;
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
        private Dictionary<string, string> Query = new Dictionary<string, string>()
        {
            { "insert-user",        "INSERT INTO person (login, pass_hash) VALUES (@Login, @PassHash)" },
            { "get-pass-hash",      "SELECT pass_hash FROM person WHERE login = @Login" },
            { "insert-token",       "INSERT INTO token (login, token) VALUES (@Login, @Token)"},
            { "get-token",          "SELECT token FROM token WHERE login = @Login" },
            { "get-login",          "SELECT login FROM token WHERE token = @Token"},
            { "insert-book",        "INSERT INTO book (content, title) VALUES (@Content, @Title)" },
            { "insert-book-login",  "INSERT INTO book_login (login, book_id) VALUES (@Login, @BookId)" },
            { "get-book-list",      "SELECT book_id, title, offset FROM book INNER JOIN book_login USING (book_id) WHERE login = @Login" },
            { "get-book",           "SELECT content FROM book WHERE book_id = @BookId" },
            { "delete-book",        "DELETE FROM book WHERE book_id = @BookId" },
            { "person-has-book",    "SELECT 1 FROM book_login WHERE book_id = @BookId AND login = @Login" }
        };
        public void InsertUser(string login, string hashedPassword)
        {

            MySqlCommand cmd = new MySqlCommand(Query["insert-user"], Conn);
            cmd.Parameters.AddWithValue("@Login", login);
            cmd.Parameters.AddWithValue("@PassHash", hashedPassword);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception(message: "Пользователь с таким логином существует.");
            }
        }

        public string GetPersonPassHash(string login)
        {
            MySqlCommand cmd = new MySqlCommand(Query["get-pass-hash"], Conn);
            cmd.Parameters.AddWithValue("@Login", login);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
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
            MySqlCommand cmd = new MySqlCommand(Query["insert-token"], Conn);
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
            MySqlCommand cmd = new MySqlCommand(Query["get-token"], Conn);
            cmd.Parameters.AddWithValue("@Login", login);
            return GetOneStringValue(cmd);
        }

        public string GetLoginFor(string token)
        {
            MySqlCommand cmd = new MySqlCommand(Query["get-login"], Conn);
            cmd.Parameters.AddWithValue("@Token", token);
            return GetOneStringValue(cmd);
        }

        public List<BookInfo> GetBookListFor(string login)
        {
            MySqlCommand cmd = new MySqlCommand(Query["get-book-list"], Conn);
            cmd.Parameters.AddWithValue("@Login", login);
            List<BookInfo> buffer = new List<BookInfo>();
            try
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        buffer.Add(new BookInfo()
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Offset = reader.GetUInt64(2)
                        });
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Ошибка в базе данных:" + e.Message);
            }
            return buffer;
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

        public void InsertBookFor(string login, string title, string content)
        {
            MySqlCommand bookCmd = new MySqlCommand(Query["insert-book"], Conn);
            bookCmd.Parameters.AddWithValue("@Title", title);
            bookCmd.Parameters.AddWithValue("@Content", content);
            try
            {
                bookCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception("Проблема с базой данных. Книга не загружена.");
            }

            long bookId = bookCmd.LastInsertedId;
            MySqlCommand bookLoginCmd = new MySqlCommand(Query["insert-book-login"], Conn);
            bookLoginCmd.Parameters.AddWithValue("@Login", login);
            bookLoginCmd.Parameters.AddWithValue("@BookId", bookId);
            try
            {
                bookLoginCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception("Проблема с базой данных. Личная информация о книгах не обновлена.");
            }
        }

        public string GetBookContent(int bookId)
        {
            MySqlCommand bookCmd = new MySqlCommand(Query["get-book"], Conn);
            bookCmd.Parameters.AddWithValue("@BookId", bookId);
            try
            {
                return GetOneStringValue(bookCmd);
            }
            catch (Exception e)
            {
                throw new Exception("Книга отсутствует в базе данных. " + e.Message);
            }
        }

        public void DeleteBook(int bookId)
        {
            MySqlCommand cmd = new MySqlCommand(Query["delete-book"], Conn);
            cmd.Parameters.AddWithValue("@BookId", bookId);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception("Этой книги не существует.");
            }
        }
        public bool HasBook(string login, int bookId)
        {
            MySqlCommand cmd = new MySqlCommand(Query["person-has-book"], Conn);
            cmd.Parameters.AddWithValue("@Login", login);
            cmd.Parameters.AddWithValue("@BookId", bookId);
            try
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read();
                }
            }
            catch (Exception)
            {
                throw new Exception("Внутренняя ошибка. Проблема с базой данных.");
            }
        }
    }
}
