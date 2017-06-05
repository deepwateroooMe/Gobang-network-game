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
                    p.Writer(CodeNum.broadcast);
                }
                Console.ReadLine();
            }
        }
    }
}
