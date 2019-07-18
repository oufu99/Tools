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

namespace CreateSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var texts = this.txt1.Text;
            texts = texts.Replace("\r\n", "\n");
            var arr = texts.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var mobiles = "";

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i].Replace(" ", "");
                mobiles += $"\"{arr[i]}\",";
            }
            if (arr.Length > 0)
            {
                mobiles = StringHelper.RemoveLastChar(mobiles);
            }

            var sql = $@"
select * from tb_user where manufacturer_id=10052 and CustId in (select tb_customerID from tb_customer where  Manufacturer_id=10052 and Mobile in ({mobiles}));
select * from   tb_customer where  Manufacturer_id=10052 and Mobile in ({mobiles})


delete from tb_user where manufacturer_id = 10052 and CustId in (select tb_customerID from tb_customer where  Manufacturer_id = 10052 and Mobile in ({ mobiles}));
delete from   tb_customer where  Manufacturer_id = 10052 and Mobile in ({ mobiles})
";
            this.txtResult.Text = sql;

        }
    }
}
