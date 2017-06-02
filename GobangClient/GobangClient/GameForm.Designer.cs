namespace GobangClient
{
    partial class GameForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rtxtState = new System.Windows.Forms.RichTextBox();
            this.rtxtRoom = new System.Windows.Forms.RichTextBox();
            this.rtxtInput = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtxtState
            // 
            this.rtxtState.Location = new System.Drawing.Point(107, 285);
            this.rtxtState.Name = "rtxtState";
            this.rtxtState.ReadOnly = true;
            this.rtxtState.Size = new System.Drawing.Size(197, 156);
            this.rtxtState.TabIndex = 3;
            this.rtxtState.Text = "";
            // 
            // rtxtRoom
            // 
            this.rtxtRoom.Location = new System.Drawing.Point(338, 285);
            this.rtxtRoom.Name = "rtxtRoom";
            this.rtxtRoom.ReadOnly = true;
            this.rtxtRoom.Size = new System.Drawing.Size(210, 156);
            this.rtxtRoom.TabIndex = 4;
            this.rtxtRoom.Text = "";
            // 
            // rtxtInput
            // 
            this.rtxtInput.Location = new System.Drawing.Point(96, 79);
            this.rtxtInput.Name = "rtxtInput";
            this.rtxtInput.Size = new System.Drawing.Size(198, 96);
            this.rtxtInput.TabIndex = 5;
            this.rtxtInput.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(371, 88);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 484);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.rtxtInput);
            this.Controls.Add(this.rtxtRoom);
            this.Controls.Add(this.rtxtState);
            this.Name = "GameForm";
            this.Text = "Game";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtxtState;
        private System.Windows.Forms.RichTextBox rtxtRoom;
        private System.Windows.Forms.RichTextBox rtxtInput;
        private System.Windows.Forms.Button btnSend;
    }
}

