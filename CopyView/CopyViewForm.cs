using Aaron.Common;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyView
{
    public partial class CopyViewForm : Form
    {

        Dictionary<string, string> dic = new Dictionary<string, string>();

        public CopyViewForm()
        {

            InitializeComponent();

            Rectangle s = Screen.GetWorkingArea(this);
            var sx = s.Width;
            var sy = s.Height;

            var hight = this.Height;
            var width = this.Width;
            //获取中间坐标  这个控件放在左边   然后高度还要再减去自己的一半才是在中间
            this.Location = new Point((sx / 2) - width - 50, (sy / 2) - (hight / 2));


            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CopyPath.txt");
            //要更新的项目
            var fileStr = File.ReadAllLines(filePath);
            foreach (var item in fileStr)
            {
                var arr = item.Split(new string[] { @";" }, StringSplitOptions.RemoveEmptyEntries);
                dic.Add(arr[0], arr[1]);
            }

        }


        private void CopyViews(object sender, EventArgs e)
        {
            foreach (var key in dic.Keys)
            {
                FileHelper.CopyDirectory(key, dic[key]);
            }
            MessageBox.Show("复制完毕");
        }

        private void CopyViewForm_Load(object sender, EventArgs e)
        {

        }

        private void OpenConfigText()
        {
            FileHelper.OpenSoft(@"D:\Tools\CopyView\CopyPath.txt");
        }

        private void Reload()
        { }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                OpenConfigText();
                return true;
            }

            //重启软件
            if (keyData == Keys.F5)
            {
                FileHelper.ReloadSoftByProjectName(Application.ProductName);
                this.Close();
                return true;
            }
            return false;
        }
    }
}
