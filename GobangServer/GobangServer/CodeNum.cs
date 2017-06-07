using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GobangServer
{
    public class CodeNum
    {
        public const string broadcast = "$199";
        public const string have_connect = "$200";
        public const string have_playing = "$201";
        public const string usewhitepiece = "$202";
        public const string useblackpiece = "$203";
        public const string time_to_play = "$204";
        public const string miss_connect = "$404";
        public const string you_are_winner = "$206";
        public const string you_are_loster = "$207";
        public static bool Is_CodeNum(string input)
        {
            Regex regex = new Regex("^\\$\\d+");
            return regex.IsMatch(input);
        }
        public static string CreatCodeNum205(int x, int y)
        {
            return "$205:" + x + "," + y;
        }
        public static bool IsCodeNum205(string input)
        {
            Regex regex = new Regex("^\\$205:\\d+,\\d+$");
            return regex.IsMatch(input);
        }
        public static Point HandleCodeNum205(string codenum205)
        {
            string point = codenum205.Split(':')[1];
            string[] stapoint = point.Split(',');
            int x = Convert.ToInt32(stapoint[0]);
            int y = Convert.ToInt32(stapoint[1]);
            return new Point(x, y);
        }
    }
    public class Point
    {
        public int x;
        public int y;
        public Point(int thisx,int thisy)
        {
            x = thisx;y = thisy;
        }
    }
}
