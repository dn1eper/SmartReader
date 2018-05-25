using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Library.Book;

namespace Library
{
    public class LibraryStorage
    {
        private string PATH => @"C:\Users\" + Environment.UserName.ToString() + @"\SmartReader";
        private string FILE_NAME => "lib.xml";

        private FileInfo file;
        private XmlSerializer serializer;
        public List<BookRecord> Books { get; private set; }

        public LibraryStorage()
        {
            // Создание директории для хранения файла, если ее нет
            if (!Directory.Exists(PATH)) Directory.CreateDirectory(PATH);
            // Определяем путь файла лицензии
            file = new FileInfo(PATH + @"\" + FILE_NAME);
            // Инициализируем сериализатор
            Type type = typeof(List<BookRecord>);
            serializer = new XmlSerializer(type);
            // Загружаем информацию о книжках из файла в память
            if (file.Exists)
            {
                using (Stream stream = file.OpenRead())
                {
                    Books = serializer.Deserialize(stream) as List<BookRecord>;
                }
            }
            else Books = new List<BookRecord>();
        }

        public bool AddBook(BookRecord book)
        {
            if (Books.Contains(book))
            {
                Books.Remove(book);
                Books.Add(book);
                return false;
            }
            else
            {
                Books.Add(book);
                return true;
            }
        }

        public BookRecord GetRecord(string path)
        {
            BookRecord bookRecord = new BookRecord()
            {
                Path = path,
                Offset = 0
            };
            if (Books.Contains(bookRecord)) return Books[Books.IndexOf(bookRecord)];
            else return bookRecord;
        }

        public void Save()
        {
            using (Stream stream = file.OpenWrite())
            {
                serializer.Serialize(stream, Books);
            }
        }

        public void Close() => Save();
    }
}
