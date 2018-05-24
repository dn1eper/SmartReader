using System;
using System.Windows.Forms;
using Library;

namespace Client.Forms
{
    public partial class MainForm : Form
    {
        private Book book;
        private bool IsBookOpend => book != null;

        private bool IsFullScreen { get; set; }

        public MainForm()
        {
            InitializeComponent();
            richTextBox.MouseWheel += new MouseEventHandler(OnMouseWheel);
            IsFullScreen = false;
        }

        private void OnExit(object sender, EventArgs e) => Close();

        private void OnAboutDialog(object sender, EventArgs e)
        {
            using (AboutBox dialog = new AboutBox())
            {
                dialog.ShowDialog();
            }
        }

        // statusLabel
        // progressBar

        private void OnFileOpen(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (book != null) book.Close();
                book = new Book(openFileDialog.FileName, richTextBox.Width, richTextBox.Height);

                OnNextPage();
                OnBookOpend();
            }
        }

        private void OnBookOpend()
        {
            addBookmarkMenuItem.Enabled = true;
            bookmarksMenuItem.Enabled = true;
            nextPageMenuItem.Enabled = true;
            previousPageMenuItem.Enabled = true;
        }

        private void OnResize(object sender, EventArgs e)
        {
            if (!IsBookOpend) return;
            richTextBox.Text = book.ReloadPage(richTextBox.Width, richTextBox.Height);
        }

        private void OnNextPage(object sender, EventArgs e) => OnNextPage();
        private void OnNextPage()
        {
            if (!IsBookOpend) return;
            richTextBox.Text = book.NextPage();
        }

        private void OnPreviousPage(object sender, EventArgs e) => OnPreviousPage();
        private void OnPreviousPage()
        {
            if (!IsBookOpend) return;
            richTextBox.Text = book.PreviousPage();
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) OnPreviousPage();
            else OnNextPage();
        }

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
    }
}
