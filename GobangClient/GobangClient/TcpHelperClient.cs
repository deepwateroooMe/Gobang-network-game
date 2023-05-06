using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using GobangClassLibrary;

namespace GobangClient
{
    public class TcpHelperClient
    {
        public static Thread ReaderThread = null;
        public static string NickName = "无名氏";
        private static TcpClient tcpclient = null;
        private static StreamReader sr = null;
        private static StreamWriter sw = null;
        private static Dictionary<string, string> codetomessage = new Dictionary<string, string>();
        public static void Init()
        {
            tcpclient = new TcpClient();
            tcpclient.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9961));
            NetworkStream tcpstream = tcpclient.GetStream();
            sr = new StreamReader(tcpstream);
            sw = new StreamWriter(tcpstream);
            ReaderThread = new Thread(new ThreadStart(ReaderThreadwork));
            ReaderThread.Start();
            initdictionary();
        }
        public static void ReaderThreadwork()
        {
            string message;
            while (true)
            {
                message = Reader();
                if (CodeNum.Is_CodeNum(message))
                {
                    HandleCodeNum(message);
                }
                else
                {
                    message = renderstringfromserver(message);
                    ControlHander.Write(CodeNum.rtxtRoom, message);
                }
            }
        }
        public static void Writer(string message)
        {
            sw.WriteLine(message);
            sw.Flush();
        }
        public static string Reader()
        {
            return sr.ReadLine();
        }
        private static void HandleCodeNum(string code)
        {
            if (CodeNum.IsCodeNum205(code))
            {
                Game.NowGame.Print(CodeNum.HandleCodeNum205(code), Game.NowGame.otherColor);
            }
            else
            {
                switch (code)
                {
                    case CodeNum.useblackpiece:
                        Game.GameBegin(CodeNum.blackpiece);
                        break;
                    case CodeNum.usewhitepiece:
                        Game.GameBegin(CodeNum.whitepiece);
                        break;
                    case CodeNum.miss_connect:
                        Game.Is_Playing = false;
                        break;
                    case CodeNum.time_to_play:
                        Game.NowGame.Is_TurnToPlay = true;
                        break;
                    case CodeNum.you_are_loster:
                        Game.Is_Playing = false;
                        Game.NowGame.Is_TurnToPlay = false;
                        break;
                    case CodeNum.you_are_winner:
                        Game.Is_Playing = false;
                        Game.NowGame.Is_TurnToPlay = false;
                        break;
                }
                ControlHander.Write(CodeNum.rtxtState, codetomessage[code]);
            }
        }
        private static string renderstringfromserver(string input)
        {
            int index = Regex.Match(input, ":").Index + 1;
            return input.Substring(1, index - 1) + "[" + DateTime.Now.ToString("HH:mm:ss") + "]\r\n" + input.Substring(index);
        }
        private static void initdictionary()
        {
            codetomessage.Add(CodeNum.broadcast, "南行五子棋祝你身体健康！");
            codetomessage.Add(CodeNum.have_connect, "成功与服务器连接，正在等待对局...");
            codetomessage.Add(CodeNum.have_playing, "对局开始！");
            codetomessage.Add(CodeNum.useblackpiece, "您执黑先行");
            codetomessage.Add(CodeNum.usewhitepiece, "您执白后行");
            codetomessage.Add(CodeNum.miss_connect, "对手掉线，请等待下一场对局");
            codetomessage.Add(CodeNum.time_to_play, "现在轮到您行子");
            codetomessage.Add(CodeNum.you_are_loster, "你输了");
            codetomessage.Add(CodeNum.you_are_winner, "你赢了");
        }
    }
}
