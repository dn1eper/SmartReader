using System;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartReader.Library.Book;
using SmartReader.Networking;
using SmartReader.Message;
using SmartReader.Networking.Events;
using SmartReader.Message.Implementations;
using SmartReader.Library.Storage;
using System.IO;

namespace SmartReader.Client.Forms
{
    // dataGridViewRows
    // [0] - №
    // [1] - Name
    // [2] - Local
    // [3] - Remote
    // [4] - Path
    // [5] - ID

    public partial class LibraryForm : Form
    {
        private LibraryStorage Storage;
        private IConnection Connection;
        private string Token;
        private List<BookRecord> LocalBooks => Storage.Books;
        private List<BookInfo> RemoteBooks;
        private bool IsConnected => Connection != null && !Token.IsEmpty();

        public BookRecord SelectedBook
        {
            get
            {
                if (IsLocalSelectedBook)
                {
                    string path = dataGridView.SelectedRows[0].Cells[4].Value as string;
                    int index = LocalBooks.IndexOf(new BookRecord() { Path = path, Offset = 0 });
                    return LocalBooks[index];
                }
                else return null;
            }
        }
        private bool IsLocalSelectedBook => Convert.ToBoolean(dataGridView.SelectedRows[0].Cells[2].Value);
        private bool IsRemoteSelectedBook => Convert.ToBoolean(dataGridView.SelectedRows[0].Cells[3].Value);
        private bool OnSelectionChangedBlock = false;

        public LibraryForm(LibraryStorage storage)
        {
            InitializeComponent();
            Storage = storage;
            RemoteBooks = new List<BookInfo>();
            dataGridView.SelectionChanged += OnSelectionChanged;

            DrawBooksTable();
        }
        public LibraryForm(LibraryStorage storage, IConnection connection, string token) : this(storage)
        {
            Connection = connection;
            Connection.MessageReceived += OnIncomingMessage;
            Token = token;
            if (Token.IsEmpty()) statusLabel.Text = "Not signed";
            else if (Connection != null) statusLabel.Text = "Online";
            // Загрузка списка книг с сервера
            LoadBookList();
        }

        #region Оконные события
        // Рисует таблицу с книгами
        private void DrawBooksTable()
        {
            OnSelectionChangedBlock = true;
            int index = -1;
            dataGridView.Rows.Clear();
            // Вывод Local Books
            while (++index < LocalBooks.Count)
            {
                dataGridView.Rows.Add(index+1, System.IO.Path.GetFileNameWithoutExtension(LocalBooks[LocalBooks.Count-index-1].Path),
                    true, false, LocalBooks[LocalBooks.Count-index-1].Path);
            }
            // Вывод Remote Books
            foreach (BookInfo book in RemoteBooks)
            {
                // Если книжка уже есть в списке, то добавляем ей флай Remote
                if (false)
                {
                    //TODO: ...
                }
                // Иначе добавляем новую строчку
                else {
                    dataGridView.Rows.Add(index++, book.Title, false, true);
                }
            }
            OnSelectionChangedBlock = false;
        }

        // Удалить книжку локально и с сервера (не файл а ссылку)
        private void OnDelete(object sender, EventArgs e)
        {
            // Если удаляемая книга находится на сервере, полылаем на сервер запрос на удаление
            if (IsRemoteSelectedBook)
            {
                DeleteRemoteBook(dataGridView.SelectedRows[0].Cells[5].Value as string);
            }
            // И удаляем ее локально
            if (IsLocalSelectedBook)
            {
                Storage.DeleteBook(SelectedBook);
            }
            dataGridView.Rows.Remove(dataGridView.SelectedRows[0]);
        }

        // Изменение выбранной строчки в таблице
        private void OnSelectionChanged(object sender, EventArgs e)
        {
            if (!OnSelectionChangedBlock)
            {
                okButton.Enabled = IsLocalSelectedBook;
                downloadButton.Enabled = IsConnected && !IsLocalSelectedBook && IsRemoteSelectedBook;
                uploadButton.Enabled = IsConnected && IsLocalSelectedBook && !IsRemoteSelectedBook;
            }
        }

        // Обновляет статус
        private void UpdateStatusLabel()
        {
            if (!IsConnected) statusLabel.Text = "";
            else statusLabel.Text = "Online";
            progressBar.Visible = false;
        }
        #endregion

        #region Сетевые события
        // Запрашивает список книг с сервера
        private void LoadBookList()
        {
            if (IsConnected)
            {                
                IMessage message = MessageFactory.MakeGetBookListMessage(Token);
                Connection.Send(message);
                statusLabel.Text = "Loading book list...";
                progressBar.Visible = true;
            }
        }

        // Выгрузка книги на сервер
        private void OnUpload(object sender, EventArgs e)
        {
            BookRecord book = SelectedBook;
            using (StreamReader stream = new StreamReader(SelectedBook.Path))
            {
                string bookText = stream.ReadToEnd();
                IMessage message = MessageFactory.MakeUploadBookMessage(
                    System.IO.Path.GetFileNameWithoutExtension(book.Path),
                    bookText,
                    Token);
                Connection.Send(message);
            }
            statusLabel.Text = "Uploading...";
        }

        // Загрузка книги с сервера
        private void OnDownload(object sender, EventArgs e)
        {
            // TODO: ...
        }

        // Удаление книги с сервера
        private void DeleteRemoteBook(string id)
        {
            // TODO: ...
        }

        // Обработка входящих сообщений от сервера (вызов нужного обработчика)
        private void OnIncomingMessage(object sender, MessageEventArgs e)
        {
            IMessage message = e.Message as IMessage;
            switch (message.Type)
            {
                case MessageTypes.BookList:
                    OnBookListMessage(message as BookListMessage);
                    break;
            }
        }

        // Обработка сообщения со списком книг на сервере
        private void OnBookListMessage(BookListMessage bookList)
        {
            RemoteBooks = bookList.Books;

            // Задержка для того чтобы успели отрисоваться локальные книги
            System.Threading.Thread.Sleep(1000);

            DrawBooksTable();
            UpdateStatusLabel();
        }
        #endregion
    }
}
