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
    class Program
    {
        
        ManualResetEvent AllDone = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            TcpHelperServer ServerMain = new TcpHelperServer();
            Thread ListenerThread = new Thread(new ThreadStart(ServerMain.AcceptPlayerConnection));
            ListenerThread.Start();
            while (true)
            {
                foreach(Player p in TcpHelperServer.QueueForWaiting)
                {
                    p.Writer(200.ToString());
                }
                Console.ReadLine();
            }
        }
    }
    public class TcpHelperServer
    {
        public static Queue<Player> QueueForWaiting = new Queue<Player>();
        private static int ServerPort = 9961;
        private static IPEndPoint ServerIPEndPoint = new IPEndPoint(IPAddress.Any, ServerPort);
        private static TcpListener TcpListener = null;
        private NetworkStream TcpStream = null;
        private StreamReader sr = null;
        private StreamWriter sw = null;

        public TcpHelperServer()
        {
            if (TcpListener == null)
            {
                TcpListener = new TcpListener(ServerIPEndPoint);
                TcpListener.Start();
            }
        }
        public void AcceptPlayerConnection()
        {
            while (true)
            {
                TcpClient newclient = TcpListener.AcceptTcpClient();
                Player player = new Player(newclient);
                QueueForWaiting.Enqueue(player);
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
    public class Player
    {
        public TcpClient TcpClient = null;
        private StreamReader sr = null;
        private StreamWriter sw = null;
        private NetworkStream TcpStream = null;
        public Player(TcpClient tcpclient)
        {
            TcpClient = tcpclient;
            TcpStream = TcpClient.GetStream();
            sr = new StreamReader(TcpStream);
            sw = new StreamWriter(TcpStream);
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
