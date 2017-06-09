using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using GobangClassLibrary;

namespace GobangServer
{
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
            while (true)
            {
                if (QueueForPlayer.Count >= 2)
                {
                    Player p1 = QueueForPlayer.Dequeue();
                    Player p2 = QueueForPlayer.Dequeue();
                    p1.Writer(CodeNum.have_playing);
                    p2.Writer(CodeNum.have_playing);
                    Game game = new Game(p1, p2);
                    ListForGame.Add(game);
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
                player.Writer(CodeNum.have_connect);
                Counter.TotalPlayer++;
                Console.WriteLine("在线人数：" + Counter.TotalPlayer);
            }
        }
    }
}
