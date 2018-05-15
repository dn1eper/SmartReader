using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;

namespace Client.Forms
{
    public partial class MainForm : Form
    {
        private Book book;
        private int PageSize => richTextBox.Width * richTextBox.Height / 14 / 14 / 4;

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
                if (book != null) book.Dispose();
                book = new Book(openFileDialog.FileName, PageSize);

                richTextBox.Text = book.NextPage();
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
            richTextBox.Text = book.ReloadPage(PageSize);
        }

        private void OnNextPage(object sender, EventArgs e)
        {
            richTextBox.Text = book.NextPage();
        }

        private void OnPreviousPage(object sender, EventArgs e)
        {
            richTextBox.Text = book.PreviousPage();
        }
    }
}
