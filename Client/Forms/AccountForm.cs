using System.Windows.Forms;

namespace SmartReader.Client.Forms
{
    public partial class AccountForm : Form
    {
        public AccountForm(string username)
        {
            InitializeComponent();
            label.Text += username;
        }
    }
}
