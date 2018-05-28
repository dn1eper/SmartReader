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

namespace SmartReader.Client.Forms
{
    public partial class LibraryForm : Form
    {
        private LibraryStorage Storage;
        private List<BookRecord> LocalBooks => Storage.Books;
        private List<BookRecord> RemoteBooks;
        private IConnection Connection;
        private string Token;
        private bool IsConnected => Connection != null && !Token.IsEmpty();
        public BookRecord SelectedBook
        {
            get
            {
                string path = dataGridView.SelectedRows[0].Cells[4].Value as string;
                int index = LocalBooks.IndexOf(new BookRecord() { Path = path, Offset = 0 });
                return LocalBooks[index];
            }
        }

        public LibraryForm(LibraryStorage storage)
        {
            InitializeComponent();
            Storage = storage;

            DrawBooksTable();
        }
        public LibraryForm(LibraryStorage storage, IConnection connection, string token) : this(storage)
        {
            Connection = connection;
            Connection.MessageReceived += OnIncomingMessage;
            Token = token;
            if (Token.IsEmpty()) statusLabel.Text = "Not signed";
            else if (Connection != null) statusLabel.Text = "Online";
            progressBar.Visible = true;

            LoadBookList();
        }

        #region Оконные события
        // Рисует таблицу с книгами
        private void DrawBooksTable(bool local = true, bool global = false)
        {
            int index = 0;
            dataGridView.Rows.Clear();
            foreach (BookRecord book in LocalBooks)
            {
                dataGridView.Rows.Add(++index, System.IO.Path.GetFileNameWithoutExtension(book.Path), local, global, book.Path);
            }
        }

        // Удалить книжку локально и с сервера (не файл а ссылку)
        private void OnDelete(object sender, EventArgs e)
        {
            BookRecord book = SelectedBook;
            dataGridView.Rows.Remove(dataGridView.SelectedRows[0]);
            Storage.DeleteBook(book);

            //if (SelectedBook.ID)
            //DeleteBook(SelectedBook.ID);
        }

        // Обновляет статус
        private void UpdateStatusLabel()
        {
            if (!IsConnected) statusLabel.Text = "";
            else statusLabel.Text = "Online";
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
            }
        }

        // Выгрузка книги на сервер
        private void OnUpload(object sender, EventArgs e)
        {

        }

        // Загрузка книги с сервера
        private void OnDownload(object sender, EventArgs e)
        {

        }

        // Удаление книги с сервера
        private void DeleteBook(int id)
        {

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
            //RemoteBooks = bookList.BookList;
            //DrawBooksTable();
            UpdateStatusLabel();
        }
        #endregion
    }
}
