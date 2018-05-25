using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library.Book;

namespace Client.Forms
{
    public partial class LibraryForm : Form
    {
        public LibraryForm(List<BookRecord> books)
        {
            InitializeComponent();
            //dataGridView.Rows.Add();
        }

        private void OnCancel(object sender, EventArgs e) => Close();

        private void OnSelect(object sender, EventArgs e)
        {
            // TODO: open selected book
            Close();
        }
    }
}
