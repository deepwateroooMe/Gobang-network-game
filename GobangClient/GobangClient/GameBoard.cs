using System.IO;
using System.Windows.Forms;
using System.Drawing;
using GobangClassLibrary;

namespace GobangClient
{
    class GameBoard
    {
        public const string White = "White";
        public const string Black = "Black";
        public static bool is_playing = false;
        public static bool is_turn_to_play = true;
        public static string mycolor = "";
        public static string othercolor = "";
        public static GameBoard main = new GameBoard();
        private string whitepath = @"C:\Users\Administrator\Desktop\Gobang-network-game\Images\White.png";
        private string blackpath = @"C:\Users\Administrator\Desktop\Gobang-network-game\Images\Black.png";
        private string chessboardpath = @"C:\Users\Administrator\Desktop\Gobang-network-game\Images\ChessBoard.jpg";
        public static Graphics chessboard = null;
        private FileStream chessboardstream = null;
        private FileStream whitestream = null;
        private FileStream blackstream = null;
        private double[] table = new double[15];
        PictureBox pbchessboard = null;
        Bitmap bitmapchessboard = null;
        public static void GameBegin(string myplececolor)
        {
            if (myplececolor == White)
            {
                mycolor = White;
                othercolor = Black;
                is_turn_to_play = false;
            } 
            else
            {
                mycolor = Black;
                othercolor = White;
            }
            is_playing = true;
        }
        public void Init(PictureBox pb)
        {
            bitmapchessboard = new Bitmap(pb.Width, pb.Height);
            chessboard = Graphics.FromImage(bitmapchessboard);
            chessboard.DrawImage(Image.FromStream(chessboardstream), new Point(0, 0));
            pbchessboard = pb;
            pb.Image = bitmapchessboard;
        }
        private GameBoard()
        {
            whitestream = new FileStream(whitepath, FileMode.Open, FileAccess.Read);
            blackstream = new FileStream(blackpath, FileMode.Open, FileAccess.Read);
            chessboardstream = new FileStream(chessboardpath, FileMode.Open, FileAccess.Read);

            table[0] = 23.0 + 17.5;
            table[14] = 535;
            for (int i = 1; i < 14; i++)
            {
                table[i] = 23.0 + 17.5 + 35 * i;
            }
        }
        public void Print(MouseEventArgs e, string WhiteOrBlack = White)
        {
            Print(e.X, e.Y, WhiteOrBlack);
        }
        public void Print(Point mousepoint, string WhiteOrBlack = White)
        {
            Print(mousepoint.X, mousepoint.Y, WhiteOrBlack);
        }
        //请务必记得把这坨屎好好重构
        public void Print(int mousex, int mousey, string WhiteOrBlack = White)
        {
            Stream nowpiece = (WhiteOrBlack == White) ? whitestream : blackstream;
            if (WhiteOrBlack == mycolor)
            {
                if (is_turn_to_play)
                {
                    mousex = get_index(mousex);
                    mousey = get_index(mousey);
                    TcpHelperClient.main.Writer(CodeNum.CreatCodeNum205(mousex, mousey));
                    chessboard.DrawImage(Image.FromStream(nowpiece),
                        new Rectangle(new Point(23 + mousex * 35 - 10, 23 + mousey * 35 - 10),
                        new Size(20, 20)));
                    pbchessboard.Image = bitmapchessboard;
                    is_turn_to_play = false;
                }
            }
            else
            {
                chessboard.DrawImage(Image.FromStream(nowpiece),
                    new Rectangle(new Point(23 + mousex * 35 - 10, 23 + mousey * 35 - 10),
                    new Size(20, 20)));
                pbchessboard.Image = bitmapchessboard;
                is_turn_to_play = true;
            }
        }
        private int get_index(int point)
        {
            for (int i = 0; i < 15; i++)
            {
                if (point < table[i])
                {
                    point = i;
                    break;
                }
            }
            return point;
        }
    }
}
