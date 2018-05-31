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
        private ConfigStorage Config;
        private IConnection Connection;
        private string Token => Config.GetValue("Token");
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
                    string owner = Config.GetValue("Username");
                    if (owner.IsEmpty()) owner = null;
                    int index = LocalBooks.IndexOf(new BookRecord() { Path = path, Offset = 0, Owner = owner });
                    return LocalBooks[index];
                }
                else return null;
            }
        }
        private bool IsLocalSelectedBook {
            get
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    return Convert.ToBoolean(dataGridView.SelectedRows[0].Cells[2].Value);
                }
                else return false;
            }
        }
        private bool IsRemoteSelectedBook
        {
            get
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    return Convert.ToBoolean(dataGridView.SelectedRows[0].Cells[3].Value);
                }
                else return false;
            }
        }

        public LibraryForm(LibraryStorage storage, ConfigStorage config, IConnection connection)
        {
            InitializeComponent();
            Storage = storage;
            Config = config;
            Connection = connection;
            dataGridView.SelectionChanged += OnSelectionChanged;

            UpdateStatusLabel();
            DrawBooksTable();

            if (IsConnected)
            {
                Connection.MessageReceived += OnIncomingMessage;
                Shown += OnShown;
            }
        }

        #region Оконные события
        // Рисует таблицу с книгами
        private void DrawBooksTable()
        {
            int index = -1;
            int count = 1;
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
            // Рисуем Local Books
            while (++index < LocalBooks.Count)
            {
                string owner = LocalBooks[LocalBooks.Count - index - 1].Owner == null ? "" : LocalBooks[LocalBooks.Count - index - 1].Owner;
                if (owner == Config.GetValue("Username"))
                    dataGridView.Rows.Add(count++, System.IO.Path.GetFileNameWithoutExtension(LocalBooks[LocalBooks.Count-index-1].Path),
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
                        dataGridView.Rows.Add(count++, book.Title, false, true, null, book.Id);
                    }
                }
            }
            // Если нет элементов нужно запретить нажатие okButton и deleteButton (при первоначальной отрисовке)
            if (dataGridView.RowCount == 0)
            {
                okButton.Enabled = false;
                deleteButton.Enabled = false;
            }
        }

        // Диалог удаления книжки 
        private void OnDelete(object sender, EventArgs e)
        {
            bool isRemote = IsRemoteSelectedBook;
            bool isLocal = IsLocalSelectedBook;

            // Если удаляемая книга находится на сервере, полылаем на сервер запрос на удаление
            if (isRemote)
            {
                if (MessageBox.Show("Do you want to delete the book from your remote library?", "Delete book",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[5].Value);
                    RemoteBooks.Remove(new BookInfo() { Id = id });
                    DeleteRemoteBook(id);
                    isRemote = false;
                }
            }
            // И удаляем ее локально
            if (isLocal)
            {
                if (MessageBox.Show("Do you want to delete the book from your local library?", "Delete book",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Storage.DeleteBook(SelectedBook);
                    isLocal = false;
                }
            }
            // Удаляем книжку из таблицы
            if (!(isLocal || isRemote)) dataGridView.Rows.Remove(dataGridView.SelectedRows[0]);
            else UpdateCheckBox(isLocal, isRemote);
        }

        // Изменение выбранной строчки в таблице
        private void OnSelectionChanged(object sender, EventArgs e)
        {
            okButton.Enabled = IsLocalSelectedBook;
            downloadButton.Enabled = IsConnected && !IsLocalSelectedBook && IsRemoteSelectedBook;
            uploadButton.Enabled = IsConnected && IsLocalSelectedBook && !IsRemoteSelectedBook;
            deleteButton.Enabled = true;
        }

        // Загрузка списка книг после инициализации формы
        private void OnShown(object sender, EventArgs e)
        {
            if (IsConnected)
            {
                RemoteBooks = null;

                // Загрузка списка книг с сервера
                for (int i = 0; i < 3 && RemoteBooks == null; i++)
                {
                    LoadBookList();
                    // Задержка для получения книжек
                    System.Threading.Thread.Sleep(500);
                }
                if (RemoteBooks != null)
                {
                    DrawBooksTable();
                }
            }
        }

        // Обновляет статус
        private void UpdateStatusLabel()
        {
            if (!IsConnected) statusLabel.Text = "";
            else statusLabel.Text = "Online";
            progressBar.Visible = false;
        }

        private void UpdateCheckBox(bool local, bool remote)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView.SelectedRows[0];
                object num = row.Cells[0].Value;
                object title = row.Cells[1].Value;
                object path = row.Cells[4].Value;
                object id = row.Cells[5].Value;
                dataGridView.Rows.Remove(row);
                dataGridView.Rows.Add(num, title, local, remote, path, id);
            }
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
            }
        }

        // Выгрузка книги на сервер
        private void OnUpload(object sender, EventArgs e)
        {
            BookRecord book = SelectedBook;
            byte[] buffer = File.ReadAllBytes(SelectedBook.Path);
            
            IMessage message = MessageFactory.MakeUploadBookMessage(
                System.IO.Path.GetFileNameWithoutExtension(book.Path),
                buffer, Token);
            Connection.Send(message);

            // fix
            UpdateCheckBox(true, true);
            //statusLabel.Text = "Uploading...";
            //progressBar.Visible = true;
        }

        // Загрузка книги с сервера
        private void OnDownload(object sender, EventArgs e)
        {
            IMessage message = MessageFactory.MakeGetBookMessage(Token, Convert.ToInt32(dataGridView.SelectedRows[0].Cells[5].Value));
            Connection.Send(message);
            //statusLabel.Text = "Downloading...";
            //progressBar.Visible = true;
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
                    //UpdateStatusLabel();
                    //OnShown(sender, new EventArgs());
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
            // 1. Сохраняем книгу как файл
            string path = @"C:\Users\" + Environment.UserName.ToString() + @"\SmartReader\" + Config.GetValue("Username");
            string fullPath = path + @"\" + book.Title + ".txt";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            File.WriteAllBytes(fullPath, book.Content);
            // 2. Добавляем ссылку на книгу в LocalStorage
            Storage.AddBook(new BookRecord() { Path = fullPath, Offset = 0, Owner = Config.GetValue("Username") });

            // 3. Отобразить ее в dataGridView (поствить галочку LocalBooks)
            //DrawBooksTable();
            UpdateCheckBox(true, true);

            MessageBox.Show("Book successfully downloaded!", "Download book.",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            //UpdateStatusLabel();
        }
        #endregion
    }
}
