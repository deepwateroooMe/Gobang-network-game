using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using GobangClassLibrary;
namespace GobangServer {
    public class TcpHelperServer {
        public static Queue<Player> QueueForPlayer = new Queue<Player>();
        public static List<Game> GameList = new List<Game>(); // 使用链表：尤其晚点儿要回收的时候，查询效率太低了，不好用
        private static TcpListener TcpListener = null;
        public Thread ListenerThread = null;      // 这个线程，专门负责接收客户端 
        public Thread GameMakerThread = null;     // 这个线程，专门负责创建游戏
        public Thread GameDestroyerThread = null; // 这个线程，专门负责游戏的销毁
        public TcpHelperServer() {
            IPEndPoint ServerIPEndPoint = new IPEndPoint(IPAddress.Any, 9961);
            TcpListener = new TcpListener(ServerIPEndPoint);
            TcpListener.Start();
            ListenerThread = new Thread(new ThreadStart(AcceptPlayerConnectionThreadwork)); // 接收客户端后的回调：创建一个终端，创建一个玩家管理 
            ListenerThread.Start();
            GameMakerThread = new Thread(new ThreadStart(MakeGameThreadwork));
            GameMakerThread.Start();
        }
        public void DestroyerGameThreadwork() {
            // 从ListForGame中找已经被遗弃的实例，【链表效率狠低】
        }
        public void MakeGameThreadwork() {
            while (true) {
                if (QueueForPlayer.Count >= 2) {
                    Player p1 = QueueForPlayer.Dequeue();
                    Player p2 = QueueForPlayer.Dequeue();
                    Game game = new Game(p1, p2);
                    GameList.Add(game);
                }
            }
        }
        public void AcceptPlayerConnectionThreadwork() {
            while (true) {
                TcpClient newclient = TcpListener.AcceptTcpClient();
                Player player = new Player(newclient);
                QueueForPlayer.Enqueue(player);
                player.Writer(CodeNum.have_connect); 
            }
        }
    }
}
