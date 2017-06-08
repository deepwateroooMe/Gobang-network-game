using System;
using System.Windows.Forms;
using GobangClassLibrary;

namespace GobangClient
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (txtNickName.Text.Trim() == "")
            {
                MessageBox.Show("请输入昵称");
            }
            else
            {
                TcpHelperClient thc = TcpHelperClient.main;
                string Nickname = txtNickName.Text.Trim();
                thc.Writer("name:" + Nickname);
                TcpHelperClient.NickName = Nickname;
                GameForm gameform = new GameForm();
                this.Hide();
                gameform.ShowDialog();
            }
        }
        private void txtNickName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ActiveControl = btnConnect;
        }
    }
}
