using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GobangClassLibrary;
using System.Drawing;

namespace GobangServer
{
    public class GameManual
    {
        public int[,] ChessManual = new int[15, 15];
        public int have_result = CodeNum.noresult;
        public void PlayChess(int indexx, int indexy, int whiteorblack)
        {
            if (ChessManual[indexx, indexy] == 0)
                ChessManual[indexx, indexy] = whiteorblack;
            if (Is_Win(indexx, indexy, whiteorblack))
            {
                have_result = whiteorblack;
            }
        }
        public void PlayChess(string codenum205, int whiteorblack)
        {
            Point point = CodeNum.HandleCodeNum205(codenum205);
            PlayChess(point.X, point.Y, whiteorblack);
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
            #region"撇"
            x = indexx; y = indexy;
            for (; x < 15 && y < 15;)
            {
                if (x >= 15 || y >= 15 || ChessManual[x, y] != whiteorblack)
                {
                    break;
                }
                havetouch++; x++; y++;
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
            #region"捺"
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
            for (; x >= 0 && y < 15;)
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
}
