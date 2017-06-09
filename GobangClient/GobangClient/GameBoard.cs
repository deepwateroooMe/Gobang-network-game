using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using GobangClassLibrary;

namespace GobangClient
{
    class Game
    {
        public static PictureBox pbChessBoard = null;
        public static Game NowGame = null;//单例
        public static bool Is_Playing = false;
        private static Bitmap bitmapchessboard;
        private static Graphics chessboard = null;
        private static Image chessboardimage;
        private static Image whiteimage;
        private static Image blackimage;
        private static double[] table = new double[15];
        public bool Is_TurnToPlay = true;
        public int myColor;
        public int otherColor;
        public static void Init(PictureBox pb)
        {
            pbChessBoard = pb;
            inittable();
            initimage();
            initgameboard();
        }
        private Game()
        {
            initgameboard();
        }
        public static void GameBegin(int myplececolor)
        {
            NowGame = new Game();
            NowGame.myColor = myplececolor;
            NowGame.otherColor = CodeNum.GetOtherSide(myplececolor);
            if (myplececolor == CodeNum.whitepiece)
            {
                NowGame.Is_TurnToPlay = false;
            }
            Is_Playing = true;
        }
        public static void GameEnd()
        {
            //waiting
        }
        public void Print(MouseEventArgs e, int WhiteOrBlack)
        {
            Print(e.X, e.Y, WhiteOrBlack);
        }
        public void Print(Point mousepoint, int WhiteOrBlack)
        {
            Print(mousepoint.X, mousepoint.Y, WhiteOrBlack);
        }
        public void Print(int mousex, int mousey, int WhiteOrBlack)
        {
            Image nowpiece = (WhiteOrBlack == CodeNum.whitepiece) ? whiteimage : blackimage;
            if (WhiteOrBlack == myColor)
            {
                mousex = get_index(mousex);
                mousey = get_index(mousey);
                TcpHelperClient.Writer(CodeNum.CreatCodeNum205(mousex, mousey));
                Is_TurnToPlay = false;
            }
            else
            {
                Is_TurnToPlay = true;
            }
            chessboard.DrawImage(nowpiece,
                new Rectangle(new Point(23 + mousex * 35 - 10, 23 + mousey * 35 - 10),
                new Size(20, 20)));
            pbChessBoard.Image = bitmapchessboard;
        }
        private static void initgameboard()
        {
            bitmapchessboard = new Bitmap(pbChessBoard.Width, pbChessBoard.Height);
            chessboard = Graphics.FromImage(bitmapchessboard);
            chessboard.DrawImage(chessboardimage, new Point(0, 0));
            pbChessBoard.Image = bitmapchessboard;
        }
        private static void initimage()
        {
            whiteimage = Image.FromFile(Environment.CurrentDirectory + @"\Images\White.png");
            blackimage = Image.FromFile(Environment.CurrentDirectory + @"\Images\Black.png");
            chessboardimage = Image.FromFile(Environment.CurrentDirectory + @"\Images\ChessBoard.jpg");
        }
        private static void inittable()
        {
            table[0] = 23.0 + 17.5;
            table[14] = 535;
            for (int i = 1; i < 14; i++)
            {
                table[i] = 23.0 + 17.5 + 35 * i;
            }
        }
        private static int get_index(int point)
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
