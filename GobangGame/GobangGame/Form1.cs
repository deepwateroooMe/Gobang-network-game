using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace GobangGame
{
    public partial class Form1 : Form
    {
        static string nowcolor = GameBoard.Black;
        public Form1()
        {
            InitializeComponent();
            GameBoard.main.Init(ChessBoard);
        }

        private void ChessBoard_Click(object sender, EventArgs e)
        {
        }

        private void ChessBoard_MouseClick(object sender, MouseEventArgs e)
        {
            GameBoard.main.Print(e, nowcolor);
            nowcolor = (nowcolor == GameBoard.Black) ? GameBoard.White : GameBoard.Black;
        }
    }
    class GameManual
    {
        public const int whitepiece = 1;
        public const int blackpiece = 2;
        public static GameManual gamemanual = new GameManual();
        public static int[,] ChessManual = new int[15, 15];
        private GameManual()
        {

        }
        public void PlayChess(int indexx, int indexy, int whiteorblack)
        {
            if (ChessManual[indexx, indexy] == 0)
                ChessManual[indexx, indexy] = whiteorblack;
            if (Is_Win(indexx, indexy, whiteorblack))
            {
                MessageBox.Show("win");
            }

        }
        private bool Is_Win(int indexx, int indexy, int whiteorblack)
        {
            int havetouch = -1;//(indexx,indexy)这个点会被算两次
            int x, y;
            #region"横"
            for (x = indexx; x < 15; x++)
            {
                if (ChessManual[x, indexy] != whiteorblack)
                {
                    break;
                }
                havetouch++;
            }
            for (x = indexx; x >= 0; x--)
            {
                if (ChessManual[x, indexy] != whiteorblack)
                {
                    break;
                }
                havetouch++;
            }
            if (havetouch >= 5)
                return true;
            havetouch = -1;
            #endregion
            #region"竖"
            for (y = indexy; y < 15; y++)
            {
                if (ChessManual[indexx, y] != whiteorblack)
                {
                    break;
                }
                havetouch++;
            }
            for (y = indexy; y >= 0; y--)
            {
                if (ChessManual[indexx, y] != whiteorblack)
                {
                    break;
                }
                havetouch++;
            }
            if (havetouch >= 5)
                return true;
            havetouch = -1;
            #endregion
            #region"正斜"
            x = indexx;y = indexy;
            for (; x < 15 && y < 15;)
            {
                if (x >= 15 || y >= 15 || ChessManual[x, y] != whiteorblack) 
                {
                    break;
                }
                havetouch++;x++;y++;
            }
            x = indexx; y = indexy;
            for (; x >= 0 && y >= 0;) 
            {
                if (x < 0 || y < 0 || ChessManual[x, y] != whiteorblack) 
                {
                    break;
                }
                havetouch++; x--; y--;
            }
            if (havetouch >= 5)
                return true;
            havetouch = -1;
            #endregion
            #region"负斜"
            x = indexx; y = indexy;
            for (; x < 15 && y >= 0;)
            {
                if (x >= 15 || y >= 15 || ChessManual[x, y] != whiteorblack)
                {
                    break;
                }
                havetouch++; x++; y--;
            }
            x = indexx; y = indexy;
            for (; x >=0 && y < 15;)
            {
                if (ChessManual[x, y] != whiteorblack)
                {
                    break;
                }
                havetouch++; x--; y++;
            }
            if (havetouch >= 5)
                return true;
            #endregion
            return false;
        }
    }
    class GameBoard
    {
        public GameManual gamemanual = GameManual.gamemanual;
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
                    chessboard.DrawImage(Image.FromStream(nowpiece),
                        new Rectangle(new Point(23 + mousex * 35 - 10, 23 + mousey * 35 - 10),
                        new Size(20, 20)));
                    pbchessboard.Image = bitmapchessboard;
                    is_turn_to_play = false;
                }
            }
            else
            {
                mousex = get_index(mousex);
                mousey = get_index(mousey);
                chessboard.DrawImage(Image.FromStream(nowpiece),
                    new Rectangle(new Point(23 + mousex * 35 - 10, 23 + mousey * 35 - 10),
                    new Size(20, 20)));
                pbchessboard.Image = bitmapchessboard;
                is_turn_to_play = true;
            }
            if (WhiteOrBlack == Black)
            {
                gamemanual.PlayChess(mousex, mousey, GameManual.blackpiece);
            }
            else
            {
                gamemanual.PlayChess(mousex, mousey, GameManual.whitepiece);
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
