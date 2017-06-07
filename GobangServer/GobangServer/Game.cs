using System;
using System.Threading;

namespace GobangServer
{
    public class Game
    {
        public bool is_playing = true;
        public Player white = null;
        public Player black = null;
        public Thread TalkerThread = null;
        public GameManual gamemanual = new GameManual();
        public Game(Player p1, Player p2)
        {
            white = p1;
            black = p2;
            TalkerThread = new Thread(new ThreadStart(TalkThreadwork));
            TalkerThread.Start();
            white.Writer(CodeNum.usewhitepiece);
            black.Writer(CodeNum.useblackpiece);
        }
        //还有这坨屎也是，谢谢
        public void TalkThreadwork()
        {
            string content;
            while (true)
            {
                if (white.is_connect)
                {
                    while (white.MessageBox.Count != 0)
                    {
                        content = white.MessageBox.Dequeue();
                        if (CodeNum.IsCodeNum205(content))
                        {
                            gamemanual.PlayChess(content, GameManual.whitepiece);
                            black.Writer(content);
                            if (gamemanual.have_result == GameManual.whitewin)
                            {
                                black.Writer(CodeNum.you_are_loster);
                                white.Writer(CodeNum.you_are_winner);
                                is_playing = false;
                                TalkerThread.Abort();
                            }
                        }
                        else
                            black.Writer("!" + white.NickName + ":" + content);
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
                            gamemanual.PlayChess(content, GameManual.blackpiece);
                            white.Writer(content);
                            if (gamemanual.have_result == GameManual.blackwin)
                            {
                                white.Writer(CodeNum.you_are_loster);
                                black.Writer(CodeNum.you_are_winner);
                                is_playing = false;
                                TalkerThread.Abort();
                            }
                        }
                        else
                            white.Writer("!" + black.NickName + ":" + content);
                    }
                }
                else
                {
                    if (white.is_connect)
                    {
                        white.Writer(CodeNum.miss_connect);
                        TcpHelperServer.QueueForPlayer.Enqueue(white);
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
