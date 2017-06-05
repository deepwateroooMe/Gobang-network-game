using System;
using System.Threading;

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
            red.Writer(CodeNum.usewhitepiece);
            black.Writer(CodeNum.useblackpiece);
        }
        //还有这坨屎也是，谢谢
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
                        if (CodeNum.IsCodeNum205(content))
                        {
                            black.Writer(content);
                        }
                        else
                            black.Writer("!" + red.NickName + ":" + content);
                    }
                }
                else
                {
                    if (black.is_connect)
                    {
                        black.Writer(CodeNum.miss_connect);
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
                        if (CodeNum.IsCodeNum205(content))
                        {
                            red.Writer(content);
                        }
                        else
                            red.Writer("!" + black.NickName + ":" + content);
                    }
                }
                else
                {
                    if (red.is_connect)
                    {
                        red.Writer(CodeNum.miss_connect);
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
