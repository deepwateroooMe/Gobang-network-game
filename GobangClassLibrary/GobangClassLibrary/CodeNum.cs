using System;
using System.Text.RegularExpressions;
using System.Drawing;


namespace GobangClassLibrary
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
        public static string CreatCodeNum205(int indexx, int indexy)
        {
            return "$205:" + indexx + "," + indexy;
        }
        public static bool IsCodeNum205(string input)
        {
            Regex regex = new Regex("^\\$205:\\d+,\\d+$");
            input += "";
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
}
