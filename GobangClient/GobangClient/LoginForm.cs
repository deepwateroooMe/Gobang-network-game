using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
