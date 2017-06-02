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

namespace GobangServer
{
    public class Player
    {
        public string NickName = "无名氏";
        public TcpClient TcpClient = null;
        private StreamReader sr = null;
        private StreamWriter sw = null;
        public NetworkStream TcpStream = null;
        public Queue<string> MessageBox = new Queue<string>();
        public Thread ReaderThread;
        public bool is_connect = true;
        public Player(TcpClient tcpclient)
        {
            TcpClient = tcpclient;
            TcpStream = TcpClient.GetStream();
            sr = new StreamReader(TcpStream);
            sw = new StreamWriter(TcpStream);
            ReaderThread = new Thread(new ThreadStart(ReadtoBoxThreadwork));
            ReaderThread.Start();
        }
        public void Writer(string message)
        {
            if (TcpClient.Connected)
            {
                sw.WriteLine(message);
                sw.Flush();
            }
            else
            {
                if (is_connect)
                {
                    Counter.TotalPlayer--;
                    Console.WriteLine(Counter.TotalPlayer);
                }
                is_connect = false;
                ReaderThread.Abort();
            }
        }
        public void ReadtoBoxThreadwork()
        {
            string message;
            try
            {
                message = sr.ReadLine();
                NickName = message.Substring(5);
            }
            catch
            {
                if (is_connect)
                {
                    Counter.TotalPlayer--;
                    Console.WriteLine("在线人数：" + Counter.TotalPlayer);
                }
                is_connect = false;
                ReaderThread.Abort();
            }
            while (true)
            {
                try
                {
                    message = sr.ReadLine();
                    MessageBox.Enqueue(message);
                }
                catch
                {
                    if (is_connect)
                    {
                        Counter.TotalPlayer--;
                        Console.WriteLine("在线人数：" + Counter.TotalPlayer);
                    }
                    is_connect = false;
                    ReaderThread.Abort();
                }
            }
        }
    }
}
