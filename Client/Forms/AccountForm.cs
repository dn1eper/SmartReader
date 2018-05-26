using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Extensions;

namespace Client.Forms
{
    public partial class AccountForm : Form
    {
        private bool CanAccept => !nameTextBox.Text.IsEmpty() && !passwordTextBox.Text.IsEmpty();

        public string Login => nameTextBox.Text;
        public string Password => passwordTextBox.Text;

        public AccountForm()
        {
            InitializeComponent();
            
        }

        private void OnChanged(object sender, EventArgs e)
        {
            okButton.Enabled = CanAccept;
        }
    }
}
