using System.Windows.Forms;
using GobangClassLibrary;

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
        public static void Write(int whichrtxt, string content)
        {
            switch (whichrtxt)
            {
                case CodeNum.rtxtRoom:
                    rtxtRoom.Invoke(setroom, content);
                    break;
                case CodeNum.rtxtState:
                    rtxtState.Invoke(setstate, content);
                    break;
            }
        }
    }
}
