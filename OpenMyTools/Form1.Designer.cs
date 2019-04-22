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
            this.AddNewManuExe = new System.Windows.Forms.Button();
            this.QueryCodeExe = new System.Windows.Forms.Button();
            this.UpdateTCFtpConfigExe = new System.Windows.Forms.Button();
            this.UpdateShortCodeExe = new System.Windows.Forms.Button();
            this.UpdateOneLineExe = new System.Windows.Forms.Button();
            this.OpenMyTools = new System.Windows.Forms.Button();
            this.CheckLogExe = new System.Windows.Forms.Button();
            this.WsBuildExe = new System.Windows.Forms.Button();
            this.CopyViewExe = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddNewManuExe
            // 
            this.AddNewManuExe.Location = new System.Drawing.Point(698, 413);
            this.AddNewManuExe.Name = "AddNewManuExe";
            this.AddNewManuExe.Size = new System.Drawing.Size(81, 25);
            this.AddNewManuExe.TabIndex = 0;
            this.AddNewManuExe.Text = "添加新厂商";
            this.AddNewManuExe.UseVisualStyleBackColor = true;
            this.AddNewManuExe.Click += new System.EventHandler(this.Btn_Click);
            // 
            // QueryCodeExe
            // 
            this.QueryCodeExe.Location = new System.Drawing.Point(240, 138);
            this.QueryCodeExe.Name = "QueryCodeExe";
            this.QueryCodeExe.Size = new System.Drawing.Size(194, 57);
            this.QueryCodeExe.TabIndex = 1;
            this.QueryCodeExe.Text = "查询密码";
            this.QueryCodeExe.UseVisualStyleBackColor = true;
            this.QueryCodeExe.Click += new System.EventHandler(this.Btn_Click);
            // 
            // UpdateTCFtpConfigExe
            // 
            this.UpdateTCFtpConfigExe.Location = new System.Drawing.Point(81, 229);
            this.UpdateTCFtpConfigExe.Name = "UpdateTCFtpConfigExe";
            this.UpdateTCFtpConfigExe.Size = new System.Drawing.Size(125, 57);
            this.UpdateTCFtpConfigExe.TabIndex = 2;
            this.UpdateTCFtpConfigExe.Text = "更新Ftp路径";
            this.UpdateTCFtpConfigExe.UseVisualStyleBackColor = true;
            this.UpdateTCFtpConfigExe.Click += new System.EventHandler(this.Btn_Click);
            // 
            // UpdateShortCodeExe
            // 
            this.UpdateShortCodeExe.Location = new System.Drawing.Point(460, 228);
            this.UpdateShortCodeExe.Name = "UpdateShortCodeExe";
            this.UpdateShortCodeExe.Size = new System.Drawing.Size(170, 58);
            this.UpdateShortCodeExe.TabIndex = 3;
            this.UpdateShortCodeExe.Text = "更新全部SQL";
            this.UpdateShortCodeExe.UseVisualStyleBackColor = true;
            this.UpdateShortCodeExe.Click += new System.EventHandler(this.Btn_Click);
            // 
            // UpdateOneLineExe
            // 
            this.UpdateOneLineExe.Location = new System.Drawing.Point(264, 228);
            this.UpdateOneLineExe.Name = "UpdateOneLineExe";
            this.UpdateOneLineExe.Size = new System.Drawing.Size(154, 58);
            this.UpdateOneLineExe.TabIndex = 4;
            this.UpdateOneLineExe.Text = "更新一行SQL";
            this.UpdateOneLineExe.UseVisualStyleBackColor = true;
            this.UpdateOneLineExe.Click += new System.EventHandler(this.Btn_Click);
            // 
            // OpenMyTools
            // 
            this.OpenMyTools.Location = new System.Drawing.Point(240, 327);
            this.OpenMyTools.Name = "OpenMyTools";
            this.OpenMyTools.Size = new System.Drawing.Size(194, 58);
            this.OpenMyTools.TabIndex = 5;
            this.OpenMyTools.Text = "打开我的代码";
            this.OpenMyTools.UseVisualStyleBackColor = true;
            this.OpenMyTools.Click += new System.EventHandler(this.Btn_Click);
            // 
            // CheckLogExe
            // 
            this.CheckLogExe.Location = new System.Drawing.Point(491, 327);
            this.CheckLogExe.Name = "CheckLogExe";
            this.CheckLogExe.Size = new System.Drawing.Size(139, 58);
            this.CheckLogExe.TabIndex = 6;
            this.CheckLogExe.Text = "查看日志";
            this.CheckLogExe.UseVisualStyleBackColor = true;
            this.CheckLogExe.Click += new System.EventHandler(this.Btn_Click);
            // 
            // WsBuildExe
            // 
            this.WsBuildExe.Location = new System.Drawing.Point(525, 137);
            this.WsBuildExe.Name = "WsBuildExe";
            this.WsBuildExe.Size = new System.Drawing.Size(170, 58);
            this.WsBuildExe.TabIndex = 7;
            this.WsBuildExe.Text = "编译微商代码";
            this.WsBuildExe.UseVisualStyleBackColor = true;
            this.WsBuildExe.Click += new System.EventHandler(this.Btn_Click);
            // 
            // CopyViewExe
            // 
            this.CopyViewExe.Location = new System.Drawing.Point(51, 327);
            this.CopyViewExe.Name = "CopyViewExe";
            this.CopyViewExe.Size = new System.Drawing.Size(122, 58);
            this.CopyViewExe.TabIndex = 8;
            this.CopyViewExe.Text = "复制静态页面";
            this.CopyViewExe.UseVisualStyleBackColor = true;
            this.CopyViewExe.Click += new System.EventHandler(this.Btn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(648, 327);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 58);
            this.button1.TabIndex = 9;
            this.button1.Text = "打开计算器";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CopyViewExe);
            this.Controls.Add(this.WsBuildExe);
            this.Controls.Add(this.CheckLogExe);
            this.Controls.Add(this.OpenMyTools);
            this.Controls.Add(this.UpdateOneLineExe);
            this.Controls.Add(this.UpdateShortCodeExe);
            this.Controls.Add(this.UpdateTCFtpConfigExe);
            this.Controls.Add(this.QueryCodeExe);
            this.Controls.Add(this.AddNewManuExe);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddNewManuExe;
        private System.Windows.Forms.Button QueryCodeExe;
        private System.Windows.Forms.Button UpdateTCFtpConfigExe;
        private System.Windows.Forms.Button UpdateShortCodeExe;
        private System.Windows.Forms.Button UpdateOneLineExe;
        private System.Windows.Forms.Button OpenMyTools;
        private System.Windows.Forms.Button CheckLogExe;
        private System.Windows.Forms.Button WsBuildExe;
        private System.Windows.Forms.Button CopyViewExe;
        private System.Windows.Forms.Button button1;
    }
}

