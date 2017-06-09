using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using GobangClassLibrary;

namespace GobangServer
{
    public class Player
    {
        public string NickName = "无名氏";
        public TcpClient TcpClient = null;
        private StreamReader sr = null;
        private StreamWriter sw = null;
        public Queue<string> MessageBox = new Queue<string>();
        public Thread ReaderThread;
        public bool Is_Connect = true;
        public Player(TcpClient tcpclient)
        {
            TcpClient = tcpclient;
            sr = new StreamReader(tcpclient.GetStream());
            sw = new StreamWriter(tcpclient.GetStream());
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
                endplayer();
            }
        }
        private void endplayer()
        {
            Is_Connect = false;
            Counter.EndPlayer();
            ReaderThread.Abort();
        }
        public void ReadtoBoxThreadwork()
        {
            string message;
            NickName = getnickname();
            while (true)
            {
                try
                {
                    message = sr.ReadLine();
                    MessageBox.Enqueue(message);
                }
                catch
                {
                    endplayer();
                }
            }
        }
        private string getnickname()
        {
            try
            {
                string message = sr.ReadLine();
                return message.Substring(5);
            }
            catch
            {
                endplayer();
                return "";
            }
        }
    }
}
