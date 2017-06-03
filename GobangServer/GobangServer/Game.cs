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
    public class Game
    {
        public bool is_playing = true;
        public Player red = null;
        public Player black = null;
        public Thread TalkerThread = null;
        public Game(Player p1, Player p2)
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
                        black.Writer("!" + red.NickName + ":" + content);
                    }
                }
                else
                {
                    if (black.is_connect)
                    {
                        black.Writer("$$404");
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
                        red.Writer("!" + black.NickName + ":" + content);
                    }
                }
                else
                {
                    if (red.is_connect)
                    {
                        red.Writer("$$404");
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
}
