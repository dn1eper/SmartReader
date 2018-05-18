using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book : IDisposable
    {
        private FileInfo file;
        private BookReader bookReader;

        private int pageWidth;
        private int pageHeight;


        /// <summary>
        /// Имя файла книжки
        /// </summary>
        public string Name => Path.GetFileNameWithoutExtension(file.Name);

        /// <summary>
        /// Открыта ли книжка
        /// </summary>
        public bool IsOpend => bookReader != null;

        /// <summary>
        /// Выполняет инициализацию книжки
        /// </summary>
        /// <param name="path">Путь к файлу книжки на диске</param>
        /// <param name="pageWidth">Ширина страницы</param>
        /// <param name="pageHeight">Высота страницы</param>
        /// <param name="offset">Смещение от начала книги</param>
        public Book(string path, int pageWidth, int pageHeight, int offset = 0)
        {
            file = new FileInfo(path);
            bookReader = new BookReader(file, offset);
            this.pageWidth = pageWidth;
            this.pageHeight = pageHeight;
        }

        /// <summary>
        /// Возвращает следующую страницу книги
        /// </summary>
        public string NextPage()
        {
            string line = bookReader.ReadLine();
            if (line == null) return @"Oops.. ¯\_(ツ)_/¯";
            else return line;
        }

        /// <summary>
        /// Возвращает предыдущую страницу книги
        /// </summary>
        public string PreviousPage()
        {
            string line = bookReader.ReadLine(-1);
            if (line == null) return @"Oops.. ¯\_(ツ)_/¯";
            else return line;
        }

        /// <summary>
        /// Возвращает текущую страницу книги нового размера
        /// </summary>
        /// <param name="pageWidth">Ширина страницы</param>
        /// <param name="pageHeight">Высота страницы</param>
        public string ReloadPage(int pageWidth, int pageHeight)
        {
            return bookReader.ReadLine(0);
        }

        /// <summary>
        /// Закрывает книжку
        /// </summary>
        public void Close()
        {
            Dispose();
            bookReader = null;
        }

        /// <summary>
        /// Сохраняет на диск информацию о книжке (смещение)
        /// </summary>
        public void Save()
        {
            // TODO
        }

        /// <summary>
        /// Освобождает все ресурсы
        /// </summary>
        public void Dispose()
        {
            if (IsOpend) bookReader.Dispose();
        }
    }
}
