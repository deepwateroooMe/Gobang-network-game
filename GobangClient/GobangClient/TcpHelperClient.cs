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
        public static TcpHelperClient main = new TcpHelperClient();
        public static string NickName = "无名氏";
        private TcpHelperClient()
        {
            tcpclient = new TcpClient();
            tcpclient.Connect(ServerIPEndPoint);
            tcpstream = tcpclient.GetStream();
            sr = new StreamReader(tcpstream);
            sw = new StreamWriter(tcpstream);
        }
        public void ThreadWakeUp()
        {
            ReaderThread = new Thread(new ThreadStart(ReaderThreadwork));
            ReaderThread.Start();
        }
        public void ReaderThreadwork()
        {
            int index;
            Regex is_sysmessage = new Regex("^\\$\\$\\d+$");
            string message;
            while (true)
            {
                message = Reader();
                if (is_sysmessage.IsMatch(message)) ;
                else
                {
                    index = Regex.Match(message, ":").Index + 1;
                    //因为第一位是服务器加上的"!"
                    message = message.Substring(1, index - 1) + "[" + DateTime.Now.ToString("HH:mm:ss") + "]\r\n" + message.Substring(index);
                    ControlHander.Write(1, message);
                }
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
