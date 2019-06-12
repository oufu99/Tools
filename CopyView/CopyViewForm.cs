using Common;
using Common.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            { @"E:\ZPCode\zp.ymt\ZP.YMT.Activity.Framework\Views\Shared", @"E:\ZPCode\zp.ymt\ZP.YMT.Admin\Views\Shared"}

        };


        public CopyViewForm()
        {
            InitializeComponent();

        }
        private void CopyViewForm_Load(object sender, EventArgs e)
        {
        }

        //路径太多,而且基本不会变,不用配置文件了直接写死
        private void WsbgCopy()
        {

            foreach (var key in dic.Keys)
            {
                FileHelper.CopyDirectory(key, dic[key]);
            }

            MessageBox.Show("复制完毕");
        }




        private void button1_Click(object sender, EventArgs e)
        {
            WsbgCopy();
        }

    }
}
