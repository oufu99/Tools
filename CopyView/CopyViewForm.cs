using Common;
using Common.IO;
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

        Dictionary<string, string> dic = new Dictionary<string, string>()
        {
            //View文件
            { @"E:\ZPCode\zp.ymt\ZP.YMT.ActivityAdmin.Basics\Views\ActivityBaseSetting",@"E:\ZPCode\zp.ymt\ZP.YMT.Admin\Views\ActivityBaseSetting"},
            //{ @"E:\ZPCode\zp.ymt\ZP.YMT.ActivityAdmin.Basics\Views\ActivityRange", @"E:\ZPCode\zp.ymt\ZP.YMT.Admin\Views\ActivityRange"},
            { @"E:\ZPCode\zp.ymt\ZP.YMT.Activity.Framework\Views\Shared", @"E:\ZPCode\zp.ymt\ZP.YMT.Admin\Views\Shared"},
            { @"E:\ZPCode\zp.ymt\ZP.YMT.ActivityAdmin.LuckDraw\Views\ActivityPrize",@"E:\ZPCode\zp.ymt\ZP.YMT.Admin\Views\ActivityPrize"}

        };


        public CopyViewForm()
        {
            InitializeComponent();

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
