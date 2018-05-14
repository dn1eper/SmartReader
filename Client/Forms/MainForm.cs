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

namespace Client.Forms
{
    public partial class MainForm : Form
    {
        private FileInfo file;

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

        // далее идут тестовые методы, в дальнейшем ини будут разнесены по классам
        private void OnFileOpen(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file = new FileInfo(openFileDialog.FileName);
                LoadBook();
            }
        }

        private void LoadBook()
        {
            using (FileStream stream = file.OpenRead())
            {
                byte[] buffer = new byte[1024];
                stream.Read(buffer, 0, 1024);
                richTextBox.Text = Encoding.Default.GetString(buffer);
            }
        }
    }
}
