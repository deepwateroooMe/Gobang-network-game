namespace GobangGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ChessBoard = new System.Windows.Forms.PictureBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.txtBoxX = new System.Windows.Forms.TextBox();
            this.txtBoxY = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ChessBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // ChessBoard
            // 
            this.ChessBoard.Image = ((System.Drawing.Image)(resources.GetObject("ChessBoard.Image")));
            this.ChessBoard.Location = new System.Drawing.Point(161, 63);
            this.ChessBoard.Name = "ChessBoard";
            this.ChessBoard.Size = new System.Drawing.Size(535, 535);
            this.ChessBoard.TabIndex = 0;
            this.ChessBoard.TabStop = false;
            this.ChessBoard.Click += new System.EventHandler(this.ChessBoard_Click);
            this.ChessBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChessBoard_MouseClick);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(741, 292);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 1;
            this.btnSet.Text = "落子";
            this.btnSet.UseVisualStyleBackColor = true;
            // 
            // txtBoxX
            // 
            this.txtBoxX.Location = new System.Drawing.Point(729, 129);
            this.txtBoxX.Name = "txtBoxX";
            this.txtBoxX.Size = new System.Drawing.Size(100, 25);
            this.txtBoxX.TabIndex = 2;
            // 
            // txtBoxY
            // 
            this.txtBoxY.Location = new System.Drawing.Point(729, 192);
            this.txtBoxY.Name = "txtBoxY";
            this.txtBoxY.Size = new System.Drawing.Size(100, 25);
            this.txtBoxY.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 681);
            this.Controls.Add(this.txtBoxY);
            this.Controls.Add(this.txtBoxX);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.ChessBoard);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ChessBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ChessBoard;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.TextBox txtBoxX;
        private System.Windows.Forms.TextBox txtBoxY;
    }
}

