using System;
using System.Windows.Forms;
using Library;

namespace Client.Forms
{
    public partial class MainForm : Form
    {
        private Book book;

        public MainForm()
        {
            InitializeComponent();
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
            richTextBox.Text = book.ReloadPage(richTextBox.Width, richTextBox.Height);
        }

        private void OnNextPage(object sender, EventArgs e) => OnNextPage();
        private void OnNextPage()
        {
            richTextBox.Text = book.NextPage();
        }

        private void OnPreviousPage(object sender, EventArgs e)
        {
            richTextBox.Text = book.PreviousPage();
        }
    }
}
