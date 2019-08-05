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
    }
}
