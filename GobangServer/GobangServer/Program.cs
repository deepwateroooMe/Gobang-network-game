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
        static void Main(string[] args)
        {
            TcpHelperServer ServerMain = new TcpHelperServer();
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
        public static Queue<Game> QueueForPlaying = new Queue<Game>();
        private static int ServerPort = 9961;
        private static IPEndPoint ServerIPEndPoint = new IPEndPoint(IPAddress.Any, ServerPort);
        private static TcpListener TcpListener = null;
        public Thread ListenerThread = null;
        public Thread GameMakerThread = null;
        public TcpHelperServer()
        {
            TcpListener = new TcpListener(ServerIPEndPoint);
            TcpListener.Start();
            ListenerThread = new Thread(new ThreadStart(AcceptPlayerConnectionThreadwork));
            ListenerThread.Start();
            GameMakerThread = new Thread(new ThreadStart(MakeGameThreadwork));
            GameMakerThread.Start();
        }
        public void MakeGameThreadwork()
        {
            while(true)
            {
                if (QueueForWaiting.Count >= 2)
                {
                    Player p1 = QueueForWaiting.Dequeue();
                    Player p2 = QueueForWaiting.Dequeue();
                    p1.Writer("GameBegin");
                    p2.Writer("GameBegin");
                    Game game = new Game(p1, p2);
                    QueueForPlaying.Enqueue(game);
                }
            }
        }
        public void AcceptPlayerConnectionThreadwork()
        {
            while (true)
            {
                TcpClient newclient = TcpListener.AcceptTcpClient();
                Player player = new Player(newclient);
                QueueForWaiting.Enqueue(player);
            }
        }
    }
    public class Game
    {
        public Player red = null;
        public Player black = null;
        public Thread TalkerThread = null;
        public Game(Player p1,Player p2)
        {
            red = p1;
            black = p2;
            TalkerThread = new Thread(new ThreadStart(TalkThreadwork));
            TalkerThread.Start();
        }
        public void TalkThreadwork()
        {
            string content;
            while (true)
            {
                while (red.MessageBox.Count != 0)
                {
                    content = red.MessageBox.Dequeue();
                    black.Writer(content);
                }
                while (black.MessageBox.Count != 0)
                {
                    content = black.MessageBox.Dequeue();
                    red.Writer(content);
                }
            }
        }
    }
    public class Player
    {
        public TcpClient TcpClient = null;
        private StreamReader sr = null;
        private StreamWriter sw = null;
        public NetworkStream TcpStream = null;
        public Queue<string> MessageBox = new Queue<string>();
        public Thread ReaderThread;
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
            sw.WriteLine(message);
            sw.Flush();
        }
        public void ReadtoBoxThreadwork()
        {
            while (true)
            {
                MessageBox.Enqueue(sr.ReadLine());
            }
        }
    }
}
