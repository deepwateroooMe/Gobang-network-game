﻿namespace GobangServer {
    using System;
    // 它就是个最简单的计数类，没什么意思 
    public class Counter {
        public static int TotalPlayer = 0;
        public static int TotalGame = 0;

        public static void CreateGame() {
            TotalGame++;
            Console.WriteLine("当前对局数：" + TotalGame);
        }
        public static void EndGame() {
            TotalGame--;
            Console.WriteLine("当前对局数：" + TotalGame);
        }
        public static void CreatePlayer() {
            TotalPlayer++;
            Console.WriteLine("在线人数：" + TotalPlayer);
        }
        public static void EndPlayer() {
            TotalPlayer--;
            Console.WriteLine("在线人数：" + TotalPlayer);
        }
    }
}
