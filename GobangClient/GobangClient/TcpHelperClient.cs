using System;
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
        private static string ServerAddress = "127.0.0.1";
        private static int ServerPort = 9961;
        private static IPAddress ServerIPAddress = IPAddress.Parse(ServerAddress);
        private static IPEndPoint ServerIPEndPoint = new IPEndPoint(ServerIPAddress, ServerPort);
        private TcpClient tcpclient = null;
        private NetworkStream tcpstream = null;
        public Thread ReaderThread = null;
        private StreamReader sr = null;
        private StreamWriter sw = null;
        public static TcpHelperClient main = new TcpHelperClient();
        public static string NickName = "无名氏";
        private TcpHelperClient()
        {
            tcpclient = new TcpClient();
            tcpclient.Connect(ServerIPEndPoint);
            tcpstream = tcpclient.GetStream();
            sr = new StreamReader(tcpstream);
            sw = new StreamWriter(tcpstream);
        }
        public void ThreadWakeUp()
        {
            ReaderThread = new Thread(new ThreadStart(ReaderThreadwork));
            ReaderThread.Start();
        }
        public void ReaderThreadwork()
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
                    //因为第一位是服务器加上的"!"
                    message = RenderStringFromServer(message);
                    ControlHander.Write(1, message);
                }
            }
        }
        private void HandleCodeNum(string code)
        {
            switch (code)
            {
                case CodeNum.broadcast:
                    ControlHander.Write(2, "南行五子棋祝你身体健康！");
                    break;
                case CodeNum.have_connect:
                    ControlHander.Write(2, "成功与服务器连接，正在等待对局...");
                    break;
                case CodeNum.have_playing:
                    ControlHander.Write(2, "对局开始！");
                    break;
                case CodeNum.useblackpiece:
                    ControlHander.Write(2, "您执黑先行");
                    GameBoard.GameBegin(GameBoard.Black);
                    break;
                case CodeNum.usewhitepiece:
                    ControlHander.Write(2, "您执白后行");
                    GameBoard.GameBegin(GameBoard.White);
                    break;
                case CodeNum.miss_connect:
                    ControlHander.Write(2, "对手掉线，请等待下一场对局");
                    GameBoard.is_playing = false;
                    break;
                case CodeNum.time_to_play:
                    ControlHander.Write(2, "现在轮到您行子");
                    GameBoard.is_turn_to_play = true;
                    break;
                case CodeNum.you_are_loster:
                    ControlHander.Write(2, "你输了");
                    GameBoard.is_playing = false;
                    GameBoard.is_turn_to_play = false;
                    break;
                case CodeNum.you_are_winner:
                    ControlHander.Write(2, "你赢了");
                    GameBoard.is_playing = false;
                    GameBoard.is_turn_to_play = false;
                    break;
                default:
                    break;
            }
            if (CodeNum.IsCodeNum205(code))
            {
                GameBoard.main.Print(CodeNum.HandleCodeNum205(code), GameBoard.othercolor);
            }
        }
        private string RenderStringFromServer(string input)
        {
            int index = Regex.Match(input, ":").Index + 1;
            return input.Substring(1, index - 1) + "[" + DateTime.Now.ToString("HH:mm:ss") + "]\r\n" + input.Substring(index);
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
