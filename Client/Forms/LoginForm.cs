using System;
using System.Windows.Forms;
using System.Text;

namespace SmartReader.Client.Forms
{
    public partial class LoginForm : Form
    {
        private bool CanAccept => !nameTextBox.Text.IsEmpty() && !passwordTextBox.Text.IsEmpty();

        public string Login => nameTextBox.Text;
        public string Password => passwordTextBox.Text;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void OnChanged(object sender, EventArgs e)
        {
            okButton.Enabled = CanAccept;
        }
    }
}
