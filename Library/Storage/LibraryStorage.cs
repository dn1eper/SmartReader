using System;
using System.Collections.Generic;
using System.IO;
using SmartReader.Library.Book;

namespace SmartReader.Library.Storage
{
    public class LibraryStorage : XmlStorage
    {
        public List<BookRecord> Books { get; private set; }

        public LibraryStorage() : 
            base(@"C:\Users\" + Environment.UserName.ToString() + @"\SmartReader",
                "lib.xml",
                typeof(List<BookRecord>))
        {
            Load();
        }

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
