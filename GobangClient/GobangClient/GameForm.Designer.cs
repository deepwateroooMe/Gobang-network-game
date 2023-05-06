namespace GobangClient {
    partial class GameForm {
        // 必需的设计器变量。
        private System.ComponentModel.IContainer components = null;
        // 清理所有正在使用的资源。
        // <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
#region Windows 窗体设计器生成的代码
        // 设计器支持所需的方法 - 不要修改
        // 使用代码编辑器修改此方法的内容。
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.rtxtState = new System.Windows.Forms.RichTextBox();
            this.rtxtRoom = new System.Windows.Forms.RichTextBox();
            this.rtxtInput = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.pbChessBoard = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbChessBoard)).BeginInit();
            this.SuspendLayout();
            // rtxtState
            this.rtxtState.Location = new System.Drawing.Point(657, 60);
            this.rtxtState.Name = "rtxtState";
            this.rtxtState.ReadOnly = true;
            this.rtxtState.Size = new System.Drawing.Size(197, 156);
            this.rtxtState.TabIndex = 3;
            this.rtxtState.Text = "";
            // rtxtRoom
            this.rtxtRoom.Location = new System.Drawing.Point(657, 280);
            this.rtxtRoom.Name = "rtxtRoom";
            this.rtxtRoom.ReadOnly = true;
            this.rtxtRoom.Size = new System.Drawing.Size(197, 156);
            this.rtxtRoom.TabIndex = 4;
            this.rtxtRoom.Text = "";
            // rtxtInput
            this.rtxtInput.Location = new System.Drawing.Point(657, 528);
            this.rtxtInput.Name = "rtxtInput";
            this.rtxtInput.Size = new System.Drawing.Size(197, 96);
            this.rtxtInput.TabIndex = 5;
            this.rtxtInput.Text = "";
            // btnSend
            this.btnSend.Location = new System.Drawing.Point(710, 499);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // pbChessBoard
            this.pbChessBoard.Image = ((System.Drawing.Image)(resources.GetObject("pbChessBoard.Image")));
            this.pbChessBoard.Location = new System.Drawing.Point(58, 79);
            this.pbChessBoard.Name = "pbChessBoard";
            this.pbChessBoard.Size = new System.Drawing.Size(535, 535);
            this.pbChessBoard.TabIndex = 7;
            this.pbChessBoard.TabStop = false;
            this.pbChessBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbChessBoard_MouseClick);
            // GameForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 682);
            this.Controls.Add(this.pbChessBoard);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.rtxtInput);
            this.Controls.Add(this.rtxtRoom);
            this.Controls.Add(this.rtxtState);
            this.Name = "GameForm";
            this.Text = "Game";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pbChessBoard)).EndInit();
            this.ResumeLayout(false);
        }
#endregion
        private System.Windows.Forms.RichTextBox rtxtState;
        private System.Windows.Forms.RichTextBox rtxtRoom;
        private System.Windows.Forms.RichTextBox rtxtInput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.PictureBox pbChessBoard;
    }
}