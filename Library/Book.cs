using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{
    public class Book : IDisposable
    {
        // TODO: добавить вывод текущей страницы и общего числа страниц.

        // TODO: переработать метод PreviousPage.
        // он делает не корректное смещение на более чем одну страницу
        // так как основывается на LinesOnPreviousPage (только предыдущая страница)

        // TODO: устранить ошибку, при которой переход на предыдущую страницу с самой
        // последней осуществляется не совсем корректно.

        private FileInfo file;
        private BookReader bookReader;

        private int pageWidth;
        private int pageHeight;
        private int LineWidth => pageWidth / 9;
        private int LinesCount => pageHeight / 22;
        private int LinesOnPreviousPage;
        private int linesOnCurrentPage;
        private int LinesOnCurrentPage
        {
            get => linesOnCurrentPage;
            set
            {
                if (value == 0) LinesOnPreviousPage = linesOnCurrentPage;
                linesOnCurrentPage = value;
            }
        }

        private string ErrorMessage => @"Oops.. ¯\_(ツ)_/¯";

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
        /// Возвращает следующую страницу книги построчно
        /// </summary>
        public IEnumerable<string> NextPageEnum()
        {
            int lineWidth = LineWidth;
            int linesCount = LinesCount;
            LinesOnCurrentPage = 0;
            string line;
            
            while (true)
            {
                line = bookReader.ReadLine();
                if (line == null)
                {
                    yield return ErrorMessage;
                    break;
                }
                else if (line == BookReader.LINE_AFTER_LAST) break;
                linesCount -= (int)Math.Ceiling((double)line.Length / lineWidth);
                if (linesCount >= 0)
                {
                    LinesOnCurrentPage++;
                    yield return line;
                    if (linesCount == 0) break;
                }
                else
                {
                    // TODO: реализовать частичный вывод строк
                    // если на экран не помещается строка целиком
                    bookReader.Offset(-1);
                    break;
                }
            }
        }

        /// <summary>
        /// Возвращает предыдущую страницу книги
        /// </summary>
        public string PreviousPage()
        {
            bookReader.Offset(-1 * (LinesOnCurrentPage + LinesOnPreviousPage));
            return NextPage();
        }

        /// <summary>
        /// Возвращает следующую страницу сплошным текстом
        /// </summary>
        public string NextPage()
        {
            if (bookReader.IsLastLine) bookReader.Offset(-1 * linesOnCurrentPage);
            return String.Concat(NextPageEnum());
        }

        /// <summary>
        /// Возвращает текущую страницу книги нового размера
        /// </summary>
        /// <param name="pageWidth">Ширина страницы</param>
        /// <param name="pageHeight">Высота страницы</param>
        public string ReloadPage(int pageWidth, int pageHeight)
        {
            this.pageWidth = pageWidth;
            this.pageHeight = pageHeight;
            bookReader.Offset(-1 * linesOnCurrentPage);
            return NextPage();
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
