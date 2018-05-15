using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Book : IDisposable
    {
        #region private varibles
        private FileInfo file;
        private FileStream stream;
        private byte[] buffer;
        private int _bufferSize;
        private int bufferSize
        {
            get => _bufferSize;
            set
            {
                if (value > 0)
                {
                    _bufferSize = value;
                    buffer = new byte[_bufferSize];
                }
            }
        }
        private long _offset;
        private long offset
        {
            get => _offset;
            set
            {
                if (value < 0) _offset = 0;
                else if (value > stream.Length) _offset = stream.Length - bufferSize;
                else _offset = value;
            }
        }
        #endregion

        public string Name => Path.GetFileNameWithoutExtension(file.Name);

        public Book(string path, int pageSize, long offset = 0)
        {
            file = new FileInfo(path);
            stream = file.OpenRead();
            bufferSize = pageSize;
            this.offset = offset;
        }

        private string ReadPage()
        {
            stream.Seek(offset, SeekOrigin.Begin);
            stream.Read(buffer, 0, bufferSize);
            return Encoding.Default.GetString(buffer);
        }

        /// <summary>
        /// Возвращает следующую страницу книги
        /// </summary>
        public string NextPage()
        {
            string page = ReadPage();
            offset += bufferSize;
            return page;
        }

        /// <summary>
        /// Возвращает предыдущую страницу книги
        /// </summary>
        public string PreviousPage()
        {
            offset -= bufferSize*2;
            string page = ReadPage();
            offset += bufferSize;
            return page;
        }

        /// <summary>
        /// Возвращает текущую страницу книги нового размера
        /// </summary>
        /// <param name="pageSize">Новый размер страницы</param>
        public string ReloadPage(int pageSize)
        {
            offset -= bufferSize;
            bufferSize = pageSize;
            string page = ReadPage();
            offset += bufferSize;
            return page;
        }



        #region IDisposable Support
        public void Dispose()
        {
            stream.Dispose();
        }
        #endregion
    }
}
