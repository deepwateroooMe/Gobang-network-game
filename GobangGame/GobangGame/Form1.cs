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
        public Form1()
        {
            InitializeComponent();
            Printer.main.Init(ChessBoard);
        }

        private void ChessBoard_Click(object sender, EventArgs e)
        {
        }

        private void ChessBoard_MouseClick(object sender, MouseEventArgs e)
        {
            Printer.main.Print(e);
        }
    }
    class Printer
    {
        public static Printer main = new Printer();
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
        public void Init(PictureBox pb)
        {
            bitmapchessboard = new Bitmap(pb.Width, pb.Height);
            chessboard = Graphics.FromImage(bitmapchessboard);
            chessboard.DrawImage(Image.FromStream(chessboardstream), new Point(0, 0));
            pbchessboard = pb;
            pb.Image = bitmapchessboard;
        }
        private Printer()
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
        public void Print(MouseEventArgs e, string WhiteOrBlack = "White")
        {
            Print(e.X, e.Y, WhiteOrBlack);
        }
        public void Print(int mousex,int mousey,string WhiteOrBlack = "White")
        {
            Stream nowpiece = (WhiteOrBlack == "White") ? whitestream : blackstream;
            mousex = get_index(mousex);
            mousey = get_index(mousey);
            if (chessboard == null)
                MessageBox.Show("");
            chessboard.DrawImage(Image.FromStream(nowpiece), 
                new Rectangle(new Point(23 + mousex * 35 - 10, 23 + mousey * 35 - 10),
                new Size(20, 20)));
            pbchessboard.Image = bitmapchessboard;
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
