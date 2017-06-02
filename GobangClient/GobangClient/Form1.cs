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
    public partial class Form1 : Form
    {
        
        public static TcpHelperClient thc;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            thc = new TcpHelperClient();
            ControlHander.Init(rtxtRoom, rtxtState);

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string[] messages = rtxtInput.Lines;
            rtxtInput.Clear();
            for (int i = 0; i < messages.Length; i++)
            {
                thc.Writer(messages[i]);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    public class ControlHander
    {
        public static RichTextBox rtxtRoom = null;
        public static RichTextBox rtxtState = null;
        public delegate void SetRichBoxCallBack(string content);
        public static SetRichBoxCallBack setroom;
        public static SetRichBoxCallBack setstate;
        public static void Init(RichTextBox richtxtRoom,RichTextBox richtxtState)
        {
            rtxtRoom = richtxtRoom;
            rtxtState = richtxtState;
            setroom = message => rtxtRoom.AppendText("\r\n" + message);
            setstate = message => rtxtState.AppendText("\r\n" + message);
        }
        /// <summary>
        /// 在控件中利用委派写入数据
        /// </summary>
        /// <param name="code">1表示rtxtRoom，2表示rtxtState</param>
        /// <param name="content"></param>
        public static void Write(int code,string content)
        {
            switch (code)
            {
                case 1:
                    rtxtRoom.Invoke(setroom, content);
                    break;
                case 2:
                    rtxtState.Invoke(setstate, content);
                    break;
                default:
                    break;
            }
        }
    }
    public class TcpHelperClient
    {
        private static string ServerAddress = "127.0.0.1";
        private static int ServerPort = 9961;
        private static IPAddress ServerIPAddress = IPAddress.Parse(ServerAddress);
        private static IPEndPoint ServerIPEndPoint = new IPEndPoint(ServerIPAddress, ServerPort);
        private TcpClient tcpclient = null;
        private NetworkStream tcpstream = null;
        public Thread ReaderThread = null;
        private StreamReader sr = null;
        private StreamWriter sw = null;

        public TcpHelperClient()
        {
            tcpclient = new TcpClient();
            tcpclient.Connect(ServerIPEndPoint);
            tcpstream = tcpclient.GetStream();
            sr = new StreamReader(tcpstream);
            sw = new StreamWriter(tcpstream);
            ReaderThread = new Thread(new ThreadStart(ReaderThreadwork));
            ReaderThread.Start();
        }
        public void ReaderThreadwork()
        {
            string message;
            while (true)
            {
                message = Reader();
                ControlHander.Write(1, message);
            }
        }
        public void Writer(string message)
        {
            sw.WriteLine(message);
            sw.Flush();
        }
        public string Reader()
        {
            return sr.ReadLine();
        }
    }
}
