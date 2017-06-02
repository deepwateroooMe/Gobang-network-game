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
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace GobangClient
{
    public class ControlHander
    {
        public static RichTextBox rtxtRoom = null;
        public static RichTextBox rtxtState = null;
        public delegate void SetRichBoxCallBack(string content);
        public static SetRichBoxCallBack setroom;
        public static SetRichBoxCallBack setstate;
        public static void Init(RichTextBox richtxtRoom, RichTextBox richtxtState)
        {
            rtxtRoom = richtxtRoom;
            rtxtState = richtxtState;
            setroom = message => rtxtRoom.AppendText(message + "\r\n");
            setstate = message => rtxtState.AppendText(message + "\r\n");
        }
        /// <summary>
        /// 在控件中利用委派写入数据
        /// </summary>
        /// <param name="code">1表示rtxtRoom，2表示rtxtState</param>
        /// <param name="content"></param>
        public static void Write(int code, string content)
        {
            switch (code)
            {
                case 1:
                    rtxtRoom.Invoke(setroom, content);
                    break;
                case 2:
                    rtxtState.Invoke(setstate, content);
                    break;
                default:
                    break;
            }
        }
    }
}
