using System;
using System.Windows.Forms;
using GobangClassLibrary;

namespace GobangClient
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
            //注册控件
            ControlHander.Init(rtxtRoom, rtxtState);
            Game.Init(pbChessBoard);
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            string[] messages = rtxtInput.Lines;
            string now;
            rtxtInput.Clear();
            for (int i = 0; i < messages.Length; i++)
            {
                now = messages[i];
                TcpHelperClient.Writer(now);
                now = TcpHelperClient.NickName + ":[" + DateTime.Now.ToString("HH:mm:ss") + "]\r\n" + now;
                ControlHander.Write(1, now);
            }
        }

        private void pbChessBoard_MouseClick(object sender, MouseEventArgs e)
        {
            if (Game.Is_Playing && Game.NowGame.Is_TurnToPlay) 
            {
                Game.NowGame.Print(e, Game.NowGame.myColor);
            }
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            TcpHelperClient.Init();
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
