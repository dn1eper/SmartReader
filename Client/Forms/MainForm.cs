using System;
using System.Windows.Forms;
using Library;
using Library.Book;
using System.Net.Sockets;
using SmartReader.Message;
using SmartReader.Networking;
using SmartReader.Networking.Events;

namespace Client.Forms
{
    public partial class MainForm : Form
    {
        // TODO: вывод в statusLabel номера страницы

        private Book book;
        private LibraryStorage Storage;
        private IConnection Connection;
        private bool IsBookOpend => book != null;

        public MainForm()
        {
            InitializeComponent();
            richTextBox.MouseWheel += new MouseEventHandler(OnMouseWheel);
            IsFullScreen = false;
            // Инициализируем локальное хранилище
            Storage = new LibraryStorage();
            // Открываем последнюю книгу
            if (Storage.Books.Count > 0)
            {
                OpenBook(Storage.Books[Storage.Books.Count - 1]);
            }
            // Пытаемся подключится к серверу
            ConnectServer();
        }

        private void OpenBook(BookRecord bookRecord)
        {
            book = new Book(bookRecord, richTextBox.Width, richTextBox.Height);
            book.BookOpend += OnBookOpend;
            book.BookClosed += OnBookClosed;
            book.Open();
            Storage.AddBook(bookRecord);
        }

        private void ConnectServer()
        {
            if (Connection == null)
            {
                try
                {
                    // Создаем подключение к серверу
                    Connection = NetworkingFactory.OpenConnection("localhost", 8080);
                    Connection.Open();
                    statusLabel.Text = "Online";
                    // Вешаем обработчики
                    Connection.MessageReceived += OnIncomingMessage;
                    Connection.Closed += OnConnectionClosed;
                }
                catch (SocketException e)
                {
                    Connection = null;
                }

            }
        }

        private void Login(string login, string password)
        {
            // TODO: Login user
        }

        private void Register(string login, string password, string email)
        {
            // TODO: register user
        }


        #region Диалоги
        // Диалог о программе
        private void OnAboutDialog(object sender, EventArgs e)
        {
            using (AboutBox dialog = new AboutBox())
            {
                dialog.ShowDialog();
            }
        }

        // Диалог библиотеки (список всех книг)
        private void OnLibraryDialog(object sender, EventArgs e)
        {
            using (LibraryForm libraryDialog = new LibraryForm(Storage.Books))
            {
                if (libraryDialog.ShowDialog() == DialogResult.OK)
                {
                    // Закрываем открытую книгу если нужно
                    if (IsBookOpend) book.Close();
                    // Открываем выбранную книгу
                    OpenBook(libraryDialog.SelectedBook);
                }
            }
        }

        // Диалог входа в акаунт
        private void OnAccountDialog(object sender, EventArgs e)
        {
            using (AccountForm accountDialog = new AccountForm())
            {
                switch (accountDialog.ShowDialog())
                {
                    case DialogResult.OK:
                        // Пытаемся залогинить пользователя
                        Login(accountDialog.Login, accountDialog.Password);
                        break;
                    case DialogResult.Abort:
                        // Значит нажата кнопка Register, открываем диалог регистрации
                        OnRegisterDialog(sender, e);
                        break;
                }
            }
        }

        // Диалог регистрации
        private void OnRegisterDialog(object sender, EventArgs e)
        {
            using (RegisterForm registerDialog = new RegisterForm())
            {
                if (registerDialog.ShowDialog() == DialogResult.OK)
                {
                    Register(registerDialog.Login, registerDialog.Password, registerDialog.Email);
                }
            }
        }
        
        // Диалог открытия txt файла книжки
        private void OnFileOpen(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Если была открыта книжка до этого, закрыаем ее
                if (book != null) book.Close();
                // Если эту книжку уже открывали, то нужно октыть ее с сохнененным смещением
                // для этого нужно получить запись для этой книжки из хранилища
                BookRecord bookRecord = Storage.GetRecord(openFileDialog.FileName);
                OpenBook(bookRecord);
            }
        }
        #endregion

        #region Обработчики событий
        // Выход из приложения
        private void OnExit(object sender, EventArgs e)
        {
            if (IsBookOpend)
            {
                book.Close();
                Storage.Save();
            }
            Close();
        }

        // Действия при открытии книжки
        private void OnBookOpend(object sender, EventArgs e)
        {
            // Отображаем первую страницу
            OnNextPage();
            // Активируем соответствующие кнопки меню
            addBookmarkMenuItem.Enabled = true;
            bookmarksMenuItem.Enabled = true;
            nextPageMenuItem.Enabled = true;
            previousPageMenuItem.Enabled = true;
        }

        // Действия при закрытии книжки
        private void OnBookClosed(object sender, EventArgs e)
        {
            Book oldBook = sender as Book;
            // Сохраняем сведения о книжке в хранилище
            Storage.AddBook(oldBook.BookRecord);
        }

        // Изменение объема текста на странице в зависимости от размера окна
        private void OnResize(object sender, EventArgs e)
        {
            if (!IsBookOpend) return;
            richTextBox.Text = book.ReloadPage(richTextBox.Width, richTextBox.Height);
        }

        // Переход на следующую страницу книги
        private void OnNextPage(object sender, EventArgs e) => OnNextPage();
        private void OnNextPage()
        {
            if (!IsBookOpend) return;
            richTextBox.Text = book.NextPage();
        }

        // Переход на предыдущую страницу книги
        private void OnPreviousPage(object sender, EventArgs e) => OnPreviousPage();
        private void OnPreviousPage()
        {
            if (!IsBookOpend) return;
            richTextBox.Text = book.PreviousPage();
        }

        // Перелистывание страниц прокруткой мыши
        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) OnPreviousPage();
            else OnNextPage();
        }

        // Поддержка полноэкранного режима
        private bool IsFullScreen { get; set; }
        private void OnFullScreen(object sender, EventArgs e)
        {
            if (IsFullScreen)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            IsFullScreen = !IsFullScreen;
        }

        // Обработка входящих сообщений от сервера
        private void OnIncomingMessage(object sender, MessageEventArgs e)
        {
            // TODO: обработка сообщений
        }

        // Обработка закрытия соеденения с сервером
        private void OnConnectionClosed(object sender, EventArgs e)
        {
            Connection = null;
            statusLabel.Text = "Disconnected";
        }
        #endregion
    }
}
