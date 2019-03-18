namespace CopyView
{
    partial class CopyViewForm
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
            this.btnWsbgCopy = new System.Windows.Forms.Button();
            this.btnMobileCopy = new System.Windows.Forms.Button();
            this.txtManuName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnWsbgCopy
            // 
            this.btnWsbgCopy.Location = new System.Drawing.Point(133, 312);
            this.btnWsbgCopy.Name = "btnWsbgCopy";
            this.btnWsbgCopy.Size = new System.Drawing.Size(108, 40);
            this.btnWsbgCopy.TabIndex = 0;
            this.btnWsbgCopy.Text = "复制wsbg";
            this.btnWsbgCopy.UseVisualStyleBackColor = true;
            this.btnWsbgCopy.Click += new System.EventHandler(this.WsbgCopy);
            // 
            // btnMobileCopy
            // 
            this.btnMobileCopy.Location = new System.Drawing.Point(530, 312);
            this.btnMobileCopy.Name = "btnMobileCopy";
            this.btnMobileCopy.Size = new System.Drawing.Size(109, 40);
            this.btnMobileCopy.TabIndex = 1;
            this.btnMobileCopy.Text = "复制mobile";
            this.btnMobileCopy.UseVisualStyleBackColor = true;
            this.btnMobileCopy.Click += new System.EventHandler(this.MobileCopy);
            // 
            // txtManuName
            // 
            this.txtManuName.Location = new System.Drawing.Point(209, 69);
            this.txtManuName.Name = "txtManuName";
            this.txtManuName.Size = new System.Drawing.Size(123, 21);
            this.txtManuName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "当前厂商:";
            // 
            // CopyViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtManuName);
            this.Controls.Add(this.btnMobileCopy);
            this.Controls.Add(this.btnWsbgCopy);
            this.Name = "CopyViewForm";
            this.Text = "CopyView";
            this.Load += new System.EventHandler(this.CopyViewForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWsbgCopy;
        private System.Windows.Forms.Button btnMobileCopy;
        private System.Windows.Forms.TextBox txtManuName;
        private System.Windows.Forms.Label label1;
    }
}

