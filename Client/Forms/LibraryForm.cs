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
            dataGridView.SelectionChanged += OnSelectionChanged;

            DrawBooksTable();
        }
        public LibraryForm(LibraryStorage storage, IConnection connection, string token) : this(storage)
        {
            Connection = connection;
            Connection.MessageReceived += OnIncomingMessage;
            Token = token;

            Shown += OnShown;
        }

        #region Оконные события
        // Рисует таблицу с книгами
        private void DrawBooksTable()
        {
            OnSelectionChangedBlock = true;
            int index = -1;
            dataGridView.Rows.Clear();
            // Рисуем Local Books
            while (++index < LocalBooks.Count)
            {
                dataGridView.Rows.Add(index+1, System.IO.Path.GetFileNameWithoutExtension(LocalBooks[LocalBooks.Count-index-1].Path),
                    true, false, LocalBooks[LocalBooks.Count-index-1].Path);
            }
            // Рисуем Remote Books
            if (RemoteBooks != null)
            {
                foreach (BookInfo book in RemoteBooks)
                {
                    // Если книжка уже есть в списке, то добавляем ей флай Remote
                    bool flag = true;
                    
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        bool isLocal = Convert.ToBoolean(dataGridView.Rows[i].Cells[2].Value);
                        string title = Convert.ToString(dataGridView.Rows[i].Cells[1].Value);
                        if (isLocal && book.Title == title)
                        {
                            string rowNum = Convert.ToString(dataGridView.Rows[i].Cells[0].Value);
                            string path = Convert.ToString(dataGridView.Rows[i].Cells[4].Value);
                            dataGridView.Rows[i].SetValues(rowNum, title, true, true, path, book.Id);
                            flag = false;
                            break;
                        }
                    }
                    // Иначе добавляем новую строчку
                    if (flag)
                    {
                        dataGridView.Rows.Add(index++, book.Title, false, true, null, book.Id);
                    }
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
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[5].Value);
                DeleteRemoteBook(id);
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

        // Загрузка списка книг после инициализации формы
        private void OnShown(object sender, EventArgs e)
        {
            if (Token.IsEmpty()) statusLabel.Text = "Not signed";
            else if (Connection != null) statusLabel.Text = "Online";

            // Загрузка списка книг с сервера
            for (int i = 0; i < 3 && RemoteBooks == null; i++)
            {
                LoadBookList();
                // Задержка для получения книжек
                System.Threading.Thread.Sleep(300);
            }
            if (RemoteBooks != null)
            {
                DrawBooksTable();
                UpdateStatusLabel();
                return;
            }
            else statusLabel.Text = "Can't load books list.";
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
            progressBar.Visible = true;
        }

        // Загрузка книги с сервера
        private void OnDownload(object sender, EventArgs e)
        {
            IMessage message = MessageFactory.MakeGetBookMessage(Token, Convert.ToInt32(dataGridView.SelectedRows[0].Cells[5].Value));
            Connection.Send(message);
            statusLabel.Text = "Downloading...";
            progressBar.Visible = true;
        }

        // Удаление книги с сервера
        private void DeleteRemoteBook(int id)
        {
            IMessage message = MessageFactory.MakeDeleteBookMessage(Token, id);
            Connection.Send(message);
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
                case MessageTypes.Book:
                    OnBookMessage(message as BookMessage);
                    break;
                case MessageTypes.Status:
                    UpdateStatusLabel();
                    break;
            }
        }

        // Обработка сообщения со списком книг на сервере
        private void OnBookListMessage(BookListMessage bookList)
        {
            RemoteBooks = bookList.Books;
        }

        // Обработка сообщения файлом книги
        private void OnBookMessage(BookMessage book)
        {
            // TODO: 
            // 1. Сохранить книгу как файл
            // 2. Добавить ссылку на нее в LocalStorage
            // 3. Отобразить ее в dataGridView (поствить галочку)

            UpdateStatusLabel();
        }
        #endregion
    }
}
