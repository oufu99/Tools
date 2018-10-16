namespace OpenMyTools
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
            this.addManuBtn = new System.Windows.Forms.Button();
            this.queryCodeBtn = new System.Windows.Forms.Button();
            this.updateFtpBtn = new System.Windows.Forms.Button();
            this.updateAllShortCodeBtn = new System.Windows.Forms.Button();
            this.updateShortCodeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addManuBtn
            // 
            this.addManuBtn.Location = new System.Drawing.Point(440, 145);
            this.addManuBtn.Name = "addManuBtn";
            this.addManuBtn.Size = new System.Drawing.Size(101, 55);
            this.addManuBtn.TabIndex = 0;
            this.addManuBtn.Text = "添加新厂商";
            this.addManuBtn.UseVisualStyleBackColor = true;
            this.addManuBtn.Click += new System.EventHandler(this.addManuBtn_Click);
            // 
            // queryCodeBtn
            // 
            this.queryCodeBtn.Location = new System.Drawing.Point(186, 145);
            this.queryCodeBtn.Name = "queryCodeBtn";
            this.queryCodeBtn.Size = new System.Drawing.Size(91, 57);
            this.queryCodeBtn.TabIndex = 1;
            this.queryCodeBtn.Text = "查询密码";
            this.queryCodeBtn.UseVisualStyleBackColor = true;
            this.queryCodeBtn.Click += new System.EventHandler(this.queryCodeBtn_Click);
            // 
            // updateFtpBtn
            // 
            this.updateFtpBtn.Location = new System.Drawing.Point(315, 145);
            this.updateFtpBtn.Name = "updateFtpBtn";
            this.updateFtpBtn.Size = new System.Drawing.Size(94, 57);
            this.updateFtpBtn.TabIndex = 2;
            this.updateFtpBtn.Text = "更新Ftp路径";
            this.updateFtpBtn.UseVisualStyleBackColor = true;
            this.updateFtpBtn.Click += new System.EventHandler(this.updateFtpBtn_Click);
            // 
            // updateAllShortCodeBtn
            // 
            this.updateAllShortCodeBtn.Location = new System.Drawing.Point(411, 244);
            this.updateAllShortCodeBtn.Name = "updateAllShortCodeBtn";
            this.updateAllShortCodeBtn.Size = new System.Drawing.Size(170, 58);
            this.updateAllShortCodeBtn.TabIndex = 3;
            this.updateAllShortCodeBtn.Text = "更新全部SQL";
            this.updateAllShortCodeBtn.UseVisualStyleBackColor = true;
            this.updateAllShortCodeBtn.Click += new System.EventHandler(this.updateAllShortCodeBtn_Click);
            // 
            // updateShortCodeBtn
            // 
            this.updateShortCodeBtn.Location = new System.Drawing.Point(171, 244);
            this.updateShortCodeBtn.Name = "updateShortCodeBtn";
            this.updateShortCodeBtn.Size = new System.Drawing.Size(154, 58);
            this.updateShortCodeBtn.TabIndex = 4;
            this.updateShortCodeBtn.Text = "更新一行SQL";
            this.updateShortCodeBtn.UseVisualStyleBackColor = true;
            this.updateShortCodeBtn.Click += new System.EventHandler(this.updateShortCodeBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.updateShortCodeBtn);
            this.Controls.Add(this.updateAllShortCodeBtn);
            this.Controls.Add(this.updateFtpBtn);
            this.Controls.Add(this.queryCodeBtn);
            this.Controls.Add(this.addManuBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addManuBtn;
        private System.Windows.Forms.Button queryCodeBtn;
        private System.Windows.Forms.Button updateFtpBtn;
        private System.Windows.Forms.Button updateAllShortCodeBtn;
        private System.Windows.Forms.Button updateShortCodeBtn;
    }
}

