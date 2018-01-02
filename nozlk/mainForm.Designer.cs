namespace nozlk
{
    partial class mainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonMakeLink = new System.Windows.Forms.Button();
            this.textBoxOperand = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonMakeLink
            // 
            this.buttonMakeLink.Location = new System.Drawing.Point(12, 102);
            this.buttonMakeLink.Name = "buttonMakeLink";
            this.buttonMakeLink.Size = new System.Drawing.Size(135, 37);
            this.buttonMakeLink.TabIndex = 2;
            this.buttonMakeLink.Text = "データリンクURL生成";
            this.buttonMakeLink.UseVisualStyleBackColor = true;
            this.buttonMakeLink.Click += new System.EventHandler(this.ButtonMakeLink_Click);
            // 
            // textBoxOperand
            // 
            this.textBoxOperand.Location = new System.Drawing.Point(12, 65);
            this.textBoxOperand.Name = "textBoxOperand";
            this.textBoxOperand.Size = new System.Drawing.Size(525, 19);
            this.textBoxOperand.TabIndex = 1;
            this.textBoxOperand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainForm_KeyDown);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(10, 160);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(56, 12);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(14, 28);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(189, 19);
            this.textBoxComment.TabIndex = 0;
            this.textBoxComment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainForm_KeyDown);
            // 
            // mainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 189);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.textBoxOperand);
            this.Controls.Add(this.buttonMakeLink);
            this.Name = "mainForm";
            this.Text = "nozlk";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.fileDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.fileDragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonMakeLink;
        private System.Windows.Forms.TextBox textBoxOperand;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox textBoxComment;
    }
}

