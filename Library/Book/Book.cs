using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Library.Book
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

        public event EventHandler BookOpend;
        public event EventHandler BookClosed;

        private int pageWidth;
        private int pageHeight;
        private int LineWidth => pageWidth / 9;
        private int LinesCount => pageHeight / 24;
        private int _offset;
        // TODO: Избыточные поля, желаетльно от них избавиться
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
        /// Полное имя файла книжки
        /// </summary>
        public string FullName => file.FullName;

        /// <summary>
        /// Открыта ли книжка
        /// </summary>
        public bool IsOpend => bookReader != null;

        /// <summary>
        /// Возвращает смещение текущей страницы от начала
        /// </summary>
        public int Offset
        {
            get
            {
                if (IsOpend) return bookReader.BaseOffset + bookReader.BufferOffset;
                else return -1;
            }
        }

        /// <summary>
        /// Возвращает запись для сохранения
        /// </summary>
        public BookRecord BookRecord
        {
            get
            {
                return new BookRecord()
                {
                    Path = FullName,
                    Offset = Offset
                };
            }
        }
        

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
            _offset = offset;
            this.pageWidth = pageWidth;
            this.pageHeight = pageHeight;
        }
        /// <summary>
        /// Выполняет инициализацию книжки
        /// </summary>
        /// <param name="record">Запись о книжке</param>
        /// <param name="pageWidth">Ширина страницы</param>
        /// <param name="pageHeight">Высота страницы</param>
        public Book(BookRecord record, int pageWidth, int pageHeight)
        {
            file = new FileInfo(record.Path);
            _offset = record.Offset;
            this.pageWidth = pageWidth;
            this.pageHeight = pageHeight;
        }


        /// <summary>
        /// Открывает книжку для чтения
        /// </summary>
        public void Open()
        {
            if (!IsOpend)
            {
                bookReader = new BookReader(file, _offset);
                BookOpend(this, new EventArgs());
            }
        }

        /// <summary>
        /// Возвращает следующую страницу книги построчно
        /// </summary>
        public IEnumerable<string> NextPageEnum()
        {
            if (IsOpend)
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
            else yield return null;
        }

        /// <summary>
        /// Возвращает предыдущую страницу книги
        /// </summary>
        public string PreviousPage()
        {
            if (!IsOpend) return null;
            bookReader.Offset(-1 * (LinesOnCurrentPage + LinesOnPreviousPage));
            return NextPage();
        }

        /// <summary>
        /// Возвращает следующую страницу сплошным текстом
        /// </summary>
        public string NextPage()
        {
            if (!IsOpend) return null;
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
            if (!IsOpend) return null;
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
            if (IsOpend)
            {
                BookClosed(this, new EventArgs());
                Dispose();
                bookReader = null;
            }
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
