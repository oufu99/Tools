using Common;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueryCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            txtId.TabIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var admin = "PT_DSe/XycOhQW_Q8Cu5tIZg_sg";
            Clipboard.SetText(admin);
        }
        /// <summary>
        /// 判断回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)//判断回车键
            {
                btn1_Click(null, new EventArgs());//触发按钮事件
                return true;
            }
            if (keyData == Keys.Escape)//Esc退出键
            {
                this.Close();
                return true;
            }
            return false;
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            string manuId = this.txtId.Text.Trim();
            int outManuId = 0;
            string sql = "";
            if (int.TryParse(manuId, out outManuId))
            {
                sql = string.Format($@"SELECT b.name,b.password FROM dbo.tb_manufacturer a LEFT JOIN tb_user b ON a.tb_manufacturerID=b.manufacturer_id WHERE a.tb_manufacturerID={manuId} AND b.system_role_id=-10");
            }
            else
            {
                sql = string.Format($@"SELECT b.name,b.password FROM dbo.tb_manufacturer a LEFT JOIN tb_user b ON a.tb_manufacturerID=b.manufacturer_id WHERE  b.name='{manuId}' AND b.system_role_id=-10");
            }
            var model = SQLHelper.Query<tb_manu>(sql);
            userTxt.Text = model.Name;
            pwdTxt.Text = model.PassWord;
            Clipboard.SetText(model.PassWord);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var sa = "WA@@@Wei315#@#WinGG";
            Clipboard.SetText(sa);
        }
    }
}
