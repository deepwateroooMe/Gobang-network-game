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
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace GobangClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpHelperClient thc = new TcpHelperClient();
            while (true)
            {
                string now = Console.ReadLine();
                if (now != "read")
                    thc.Writer(now);
                else
                    Console.WriteLine(thc.Reader());
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
        private StreamReader sr = null;
        private StreamWriter sw = null;

        public TcpHelperClient()
        {
            tcpclient = new TcpClient();
            tcpclient.Connect(ServerIPEndPoint);
            tcpstream = tcpclient.GetStream();
            sr = new StreamReader(tcpstream);
            sw = new StreamWriter(tcpstream);
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
