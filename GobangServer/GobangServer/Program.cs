using System;
using GobangClassLibrary;

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
                    p.Writer(CodeNum.broadcast);
                }
                Console.ReadLine();
            }
        }
    }
}
