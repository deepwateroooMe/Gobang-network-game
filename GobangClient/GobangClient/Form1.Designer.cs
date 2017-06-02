namespace GobangClient
{
    partial class Form1
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtxtState = new System.Windows.Forms.RichTextBox();
            this.rtxtRoom = new System.Windows.Forms.RichTextBox();
            this.rtxtInput = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(177, 83);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(177, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(127, 25);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "昵称:";
            // 
            // rtxtState
            // 
            this.rtxtState.Location = new System.Drawing.Point(107, 285);
            this.rtxtState.Name = "rtxtState";
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
            this.rtxtInput.Location = new System.Drawing.Point(107, 152);
            this.rtxtInput.Name = "rtxtInput";
            this.rtxtInput.Size = new System.Drawing.Size(198, 96);
            this.rtxtInput.TabIndex = 5;
            this.rtxtInput.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(373, 180);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 484);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.rtxtInput);
            this.Controls.Add(this.rtxtRoom);
            this.Controls.Add(this.rtxtState);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtxtState;
        private System.Windows.Forms.RichTextBox rtxtRoom;
        private System.Windows.Forms.RichTextBox rtxtInput;
        private System.Windows.Forms.Button btnSend;
    }
}

