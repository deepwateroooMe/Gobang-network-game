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
        static TcpHelperServer ServerMain;
        static void Main(string[] args)
        {
            ServerMain = new TcpHelperServer();
            while (true)
            {
                foreach(Player p in TcpHelperServer.QueueForPlayer)
                {
                    p.Writer("199");
                }
                Console.ReadLine();
            }
        }
    }
    public class Counter
    {
        public static int TotalPlayer = 0;
        public static int TotalGame = 0;
    }
    public class TcpHelperServer
    {
        public static Queue<int> queue { get; set; }
        public static Queue<Player> QueueForPlayer = new Queue<Player>();
        public static List<Game> ListForGame = new List<Game>();
        private static int ServerPort = 9961;
        private static IPEndPoint ServerIPEndPoint = new IPEndPoint(IPAddress.Any, ServerPort);
        private static TcpListener TcpListener = null;
        public Thread ListenerThread = null;
        public Thread GameMakerThread = null;
        public Thread GameDestroyerThread = null;
        public TcpHelperServer()
        {
            TcpListener = new TcpListener(ServerIPEndPoint);
            TcpListener.Start();
            ListenerThread = new Thread(new ThreadStart(AcceptPlayerConnectionThreadwork));
            ListenerThread.Start();
            GameMakerThread = new Thread(new ThreadStart(MakeGameThreadwork));
            GameMakerThread.Start();
        }
        public void DestroyerGameThreadwork()
        {
            //从ListForGame中找已经被遗弃的实例
        }
        public void MakeGameThreadwork()
        {
            while(true)
            {
                if (QueueForPlayer.Count >= 2)
                {
                    Player p1 = QueueForPlayer.Dequeue();
                    Player p2 = QueueForPlayer.Dequeue();
                    p1.Writer("201");
                    p2.Writer("201");
                    Game game = new Game(p1, p2);
                    ListForGame.Add(game);
                    Counter.TotalGame++;
                    Console.WriteLine("当前对局数：" + Counter.TotalGame);
                }
            }
        }
        public void AcceptPlayerConnectionThreadwork()
        {
            while (true)
            {
                TcpClient newclient = TcpListener.AcceptTcpClient();
                Player player = new Player(newclient);
                QueueForPlayer.Enqueue(player);
                player.Writer("200");
                Counter.TotalPlayer++;
                Console.WriteLine("在线人数：" + Counter.TotalPlayer);
            }
        }
    }
    public class Game
    {
        public bool is_playing = true;
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
                if (red.is_connect)
                {
                    while (red.MessageBox.Count != 0)
                    {
                        content = red.MessageBox.Dequeue();
                        black.Writer(content);
                    }
                }
                else
                {
                    if (black.is_connect)
                    {
                        black.Writer("404");
                        TcpHelperServer.QueueForPlayer.Enqueue(black);
                    }
                    is_playing = false;
                    Counter.TotalGame--;
                    Console.WriteLine("当前对局数：" + Counter.TotalGame);
                    TalkerThread.Abort();
                }
                if (black.is_connect)
                {
                    while (black.MessageBox.Count != 0)
                    {
                        content = black.MessageBox.Dequeue();
                        red.Writer(content);
                    }
                } 
                else
                {
                    if(red.is_connect)
                    {
                        red.Writer("404");
                        TcpHelperServer.QueueForPlayer.Enqueue(red);
                    }
                    is_playing = false;
                    Counter.TotalGame--;
                    Console.WriteLine("当前对局数：" + Counter.TotalGame);
                    TalkerThread.Abort();
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
            while (true)
            {
                try
                {
                    MessageBox.Enqueue(sr.ReadLine());
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
