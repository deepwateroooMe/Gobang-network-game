using System;
using System.Threading;
using GobangClassLibrary;

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
                        dealmessagefromplayer(white, content);
                    }
                }
                else
                {
                    dealwithmisconnection(white);
                }
                if (black.is_connect)
                {
                    while (black.MessageBox.Count != 0)
                    {
                        content = black.MessageBox.Dequeue();
                        dealmessagefromplayer(black, content);
                    }
                }
                else
                {
                    dealwithmisconnection(black);
                }
            }
        }
        private void dealmessagefromplayer(Player whosendmessage, string message)
        {
            Player other = getotherplayer(whosendmessage);
            if (CodeNum.IsCodeNum205(message))
            {
                dealwithcodenum205(whosendmessage, message);
            }
            else
                other.Writer("!" + whosendmessage.NickName + ":" + message);
        }
        private void dealwithcodenum205(Player whocodefrom, string codenum205)
        {
            int piece = (whocodefrom == black) ? GameManual.blackpiece : GameManual.whitepiece;
            Player other = getotherplayer(whocodefrom);
            gamemanual.PlayChess(codenum205, piece);
            other.Writer(codenum205);
            if (gamemanual.have_result == piece)
            {
                whocodefrom.Writer(CodeNum.you_are_winner);
                other.Writer(CodeNum.you_are_loster);
                abortthisgame();
                //这里在下次更新时让两方回到等待队列

            }
        }
        private void dealwithmisconnection(Player whomisconnection)
        {
            Player other = getotherplayer(whomisconnection);
            if(other.is_connect)
            {
                other.Writer(CodeNum.miss_connect);
                TcpHelperServer.QueueForPlayer.Enqueue(other);
            }
            abortthisgame();
        }
        private void abortthisgame()
        {
            is_playing = false;
            TalkerThread.Abort();
            Counter.TotalGame--;
            Console.WriteLine("当前对局数：" + Counter.TotalGame);
        }
        private Player getotherplayer(Player playernow)
        {
            return (playernow == black) ? white : black;
        }
    }
}
