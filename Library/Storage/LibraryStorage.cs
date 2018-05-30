using System;
using System.Collections.Generic;
using System.IO;
using SmartReader.Library.Book;

namespace SmartReader.Library.Storage
{
    public class LibraryStorage : XmlStorage
    {
        /// <summary>
        /// Информация о книжках в локальном хранилище
        /// </summary>
        public List<BookRecord> Books { get; private set; }

        /// <summary>
        /// Локальное хранилище книжек
        /// </summary>
        public LibraryStorage() : 
            base(@"C:\Users\" + Environment.UserName.ToString() + @"\SmartReader",
                "lib.xml",
                typeof(List<BookRecord>))
        {
            Load();
        }

        /// <summary>
        /// Загружает информацию о книжках с диска в буфер
        /// </summary>
        public override void Load()
        {
            if (file.Exists)
            {
                using (Stream stream = file.OpenRead())
                {
                    Books = serializer.Deserialize(stream) as List<BookRecord>;
                }
            }
            else Books = new List<BookRecord>();
        }

        /// <summary>
        /// Добавляет или обновляет информацию о книжке в локальном хранилище
        /// </summary>
        /// <param name="book">Книга</param>
        /// <returns>False если книжка уже была в хранилице, True - если небыло.</returns>
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

        /// <summary>
        /// Удаляет информацию о книжке из локального хранилица
        /// </summary>
        /// <param name="book">Книга</param>
        /// <returns>False если данной книжки небыло в хранилище, True если была.</returns>
        public bool DeleteBook(BookRecord book)
        {
            if (Books.Contains(book))
            {
                Books.Remove(book);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Возвращает запись о книжке
        /// </summary>
        /// <param name="path">Путь к книжке на диске</param>
        /// <param name="owner">Владелец книжки</param>
        public BookRecord GetRecord(string path, string owner = null)
        {
            BookRecord bookRecord = new BookRecord()
            {
                Path = path,
                Offset = 0,
                Owner = owner
            };
            if (Books.Contains(bookRecord)) return Books[Books.IndexOf(bookRecord)];
            else return bookRecord;
        }

        /// <summary>
        /// Сохраняет на диск из буфера информацию о всех книжках
        /// </summary>
        public override void Save()
        {
            file.Delete();
            using (Stream stream = file.OpenWrite())
            {
                serializer.Serialize(stream, Books);
            }
        }
    }
}
