using Aaron.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenBatchSoftware
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var list = new List<string>()
            {
                @"E:\ZP4.0\ZP.Framework\ZP.Framework.sln",
                @"E:\ZP4.0\ZP.Payment\ZP.Payment.sln",
                @"E:\ZP4.0\ZP.PayType\ZP.PayType.sln"
            };
            foreach (var item in list)
            {
                FileHelper.OpenSoft(item);
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var list = new List<string>()
            {
                @"E:\ZPCode\Mifei_Common_v2\Mifei_Common_v2.sln",
                @"E:\ZPCode\Mifei_v2\Mifei_v2.sln",
                @"E:\ZPCode\WsBg\WsBg.sln"
            };
            foreach (var item in list)
            {
                FileHelper.OpenSoft(item);
            }
            this.Close();
        }
    }
}
