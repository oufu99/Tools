using Common;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        public static List<string> list;
        public static List<Button> bList;
        public static List<Button> pwdList;
        public static List<Button> openList;
        private void Form1_Load(object sender, EventArgs e)
        {
            string listJson = XMLHelper.GetNodeText(XMLPath.OldPwdQuery);
            list = JsonHelper.DeserializeObject<List<string>>(listJson);
            bList = new List<Button>() { manu1Btn, manu2Btn, manu3Btn, manu4Btn, manu5Btn };
            pwdList = new List<Button>() { pwd1Btn, pwd2Btn, pwd3Btn, pwd4Btn, pwd5Btn };
            openList = new List<Button>() { open1, open2, open3, open4, open5 };
            //初始化右边五个按键的字
            string sql = string.Format($@"SELECT  a.tb_manufacturerID,a.name,b.name AS domain FROM dbo.tb_manufacturer a LEFT JOIN tb_user b ON a.tb_manufacturerID=b.manufacturer_id WHERE tb_manufacturerID in({string.Join(",", list)}) AND b.system_role_id=-10");
            var models = SQLHelper.QueryList<tb_manu>(sql);
            for (int i = 0; i < list.Count; i++)
            {
                bList[i].Text = list[i] + $"({models.First(c => c.tb_manufacturerID == list[i]).Name.Substring(0, 5)})";
                pwdList[i].Text = list[i] + $"(获取能用的代理)";
                openList[i].Text = list[i] + $"(打开admin)";
                //直接在这绑定事件 不用去后台一个个添加了
                bList[i].Click += manuBtn_Click;
                pwdList[i].Click += pwdBtn_Click;
                openList[i].Click += openUrl_Click;
            }
            for (int i = list.Count; i < bList.Count; i++)
            {
                bList[i].Hide();
                pwdList[i].Hide();
                openList[i].Hide();
            }
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
            //如果转换失败
            if (!int.TryParse(manuId, out outManuId))
            {
                sql = string.Format($@"SELECT TOP 1 a.tb_manufacturerID FROM dbo.tb_manufacturer a LEFT JOIN tb_user b ON a.tb_manufacturerID=b.manufacturer_id WHERE  b.name like '%{manuId}%' AND b.system_role_id=-10");
                var model = SQLHelper.Query<tb_manu>(sql);
                manuId = model.tb_manufacturerID;
            }
            GetManuPwd(manuId);
            XMLHelper.UpdateXMLList(list, manuId, XMLPath.OldPwdQuery);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var sa = "WA@@@Wei315#@#WinGG";
            Clipboard.SetText(sa);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string manuId = this.txtId.Text.Trim();
            int outManuId = 0;
            string sql = "";
            if (!int.TryParse(manuId, out outManuId))
            {
                sql = string.Format($@"SELECT TOP 1 a.tb_manufacturerID FROM dbo.tb_manufacturer a LEFT JOIN tb_user b ON a.tb_manufacturerID=b.manufacturer_id WHERE b.name like '%{manuId}%'");
                manuId = SQLHelper.Query<tb_manu>(sql).tb_manufacturerID;
            }
            GetCustomerPwd(manuId);
            XMLHelper.UpdateXMLList(list, manuId, XMLPath.OldPwdQuery);
        }

        private void manuBtn_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var maunId = btn.Text.GetFirstInt();
            GetManuPwd(maunId);
        }

        /// <summary>
        /// 获取能用的代理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pwdBtn_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var maunId = btn.Text.GetFirstInt();
            GetCustomerPwd(maunId);
        }

        /// <summary>
        /// 点击打开对应的网页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openUrl_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var manuId = btn.Text.GetFirstInt();
            openUrlByManuId(manuId);
        }

        private void openUrlByManuId(string maunId)
        {
            var chromePath = XMLHelper.GetNodeText(XMLPath.ChromePath);
            string sql = string.Format($@"SELECT name FROM tb_user WHERE manufacturer_id={maunId} AND system_role_id=-10");
            var model = SQLHelper.Query<tb_manu>(sql);
            var domain = model.Name;
            string myArgs = $"{domain}.onlyid.cn/SysAdmin/Tools/Login.ashx?domain={domain}&manuId={maunId}";
            ProcessStartInfo startInfo = new ProcessStartInfo(chromePath);
            startInfo.Arguments = myArgs;
            Process.Start(startInfo);
        }

        private void GetManuPwd(string manuId)
        {
            var sql = string.Format($@"SELECT b.name,b.password FROM dbo.tb_manufacturer a LEFT JOIN tb_user b ON a.tb_manufacturerID=b.manufacturer_id WHERE a.tb_manufacturerID={manuId} AND b.system_role_id=-10");
            var model = SQLHelper.Query<tb_manu>(sql);
            txtManuId.Text = manuId;
            userTxt.Text = model.Name;
            pwdTxt.Text = model.PassWord;
            Clipboard.SetText(model.PassWord);
        }

        private void GetCustomerPwd(string manuId)
        {
            var sql = string.Format(@"SELECT  TOP 1  b.name,b.password
FROM    tb_customer_{0} a
        LEFT JOIN tb_user b ON a.tb_customerID = b.custid
                               AND b.manufacturer_id = {0}
WHERE a.audit_status=1 AND b.status=0", manuId);
            var model = SQLHelper.Query<tb_manu>(sql);
            txtManuId.Text = manuId;
            userTxt.Text = model.Name;
            pwdTxt.Text = model.PassWord;
            Clipboard.SetText(model.Name);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string manuId = this.txtId.Text.Trim();
            int outManuId = 0;
            string sql = "";
            if (!int.TryParse(manuId, out outManuId))
            {
                sql = string.Format($@"SELECT TOP 1 a.tb_manufacturerID FROM dbo.tb_manufacturer a LEFT JOIN tb_user b ON a.tb_manufacturerID=b.manufacturer_id WHERE b.name like '%{manuId}%'");
                manuId = SQLHelper.Query<tb_manu>(sql).tb_manufacturerID;
            }
            openUrlByManuId(manuId);
            XMLHelper.UpdateXMLList(list, manuId, XMLPath.OldPwdQuery);
        }
    }
}
