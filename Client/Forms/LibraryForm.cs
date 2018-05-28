using System;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartReader.Library.Book;
using SmartReader.Networking;
using SmartReader.Message;

namespace SmartReader.Client.Forms
{
    public partial class LibraryForm : Form
    {
        private List<BookRecord> Books;
        private IConnection Connection;
        private string Token;
        private bool IsConnected => Connection != null && !Token.IsEmpty();
        public BookRecord SelectedBook
        {
            get
            {
                string path = dataGridView.SelectedRows[0].Cells[2].Value as string;
                int index = Books.IndexOf(new BookRecord() { Path = path, Offset = 0 });
                return Books[index];
            }
        }

        public LibraryForm(List<BookRecord> books)
        {
            InitializeComponent();
            Books = books;
            Books.Reverse();

            DrawBooksTable();
        }
        public LibraryForm(List<BookRecord> books, IConnection connection, string token) : this(books)
        {
            Connection = connection;
            Token = token;
            if (Token.IsEmpty()) statusLabel.Text = "Not signed";
            else if (Connection != null) statusLabel.Text = "Online";
            
            LoadBookList();
        }


        // Рисует таблицу с книгами
        private void DrawBooksTable()
        {
            int index = 0;
            foreach (BookRecord book in Books)
            {
                dataGridView.Rows.Add(++index, System.IO.Path.GetFileNameWithoutExtension(book.Path), true, false, book.Path);
            }
        }

        // Загружает список книг на сервере
        private void LoadBookList()
        {
            if (IsConnected)
            {
                statusLabel.Text = "Loading book list...";
                IMessage message = MessageFactory.MakeGetBookListMessage(Token);
                Connection.Send(message);
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

        // Удалить книжку из сервера и локально (не файл а ссылку)
        private void OnDelete(object sender, EventArgs e)
        {

        }
    }
}
