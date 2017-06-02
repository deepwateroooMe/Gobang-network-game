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
    public partial class GameForm : Form
    {
        public static TcpHelperClient thc;
        public GameForm()
        {
            InitializeComponent();
            ControlHander.Init(rtxtRoom, rtxtState);//这个初始化方法必须放在实例化TcpHelperServer的前面，否则实例化后Reader线程启动，而初始化还没有进行，程序崩溃
            thc = TcpHelperClient.main;
            thc.ThreadWakeUp();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string[] messages = rtxtInput.Lines;string now;
            rtxtInput.Clear();
            for (int i = 0; i < messages.Length; i++)
            {
                now = messages[i];
                thc.Writer(now);
                now = TcpHelperClient.NickName + ":[" + DateTime.Now.ToString("HH:mm:ss") + "]\r\n" + now;
                ControlHander.Write(1, now);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
