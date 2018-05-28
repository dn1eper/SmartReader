using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartReader.Library.Book;

namespace SmartReader.Client.Forms
{
    public partial class LibraryForm : Form
    {
        public BookRecord SelectedBook
        {
            get
            {
                string path = dataGridView.SelectedRows[0].Cells[2].Value as string;
                int index = Books.IndexOf(new BookRecord() { Path = path, Offset = 0 });
                return Books[index];
            }
        }

        private List<BookRecord> Books;

        public LibraryForm(List<BookRecord> books)
        {
            InitializeComponent();
            Books = books;
            Books.Reverse();
            // Создаем таблицу с книгами
            int index = 0;
            foreach (BookRecord book in Books)
            {
                dataGridView.Rows.Add(++index, System.IO.Path.GetFileNameWithoutExtension(book.Path), book.Path);
            }
        }
    }
}
