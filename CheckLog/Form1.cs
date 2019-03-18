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

namespace CheckLog
{
    public partial class Form1 : Form
    {
        private string chromePath = XMLHelper.GetNodeText(XMLPath.ChromePath);
        public Form1()
        {
            InitializeComponent();
            txt1.TabIndex = 0;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var urlHost = GetUrlHost();
            var date = GetDateTime();
            ProcessStartInfo startInfo = new ProcessStartInfo(chromePath);
            string myArgs = string.Format($"{urlHost}/Logger/WeiXinLog/Logs_{date}.txt");
            startInfo.Arguments = myArgs;
            Process.Start(startInfo);
        }


        private void button2_Click(object sender, EventArgs e)
        {

            var urlHost = GetUrlHost();
            var date = GetDateTime();
            ProcessStartInfo startInfo = new ProcessStartInfo(chromePath);
            string myArgs = string.Format($"{urlHost}/Logger/AliPay/Logs_{date}.txt");
            startInfo.Arguments = myArgs;
            Process.Start(startInfo);
        }


        private string GetDomain()
        {
            string domain = this.txt1.Text.Trim();
            int outManuId = 0;
            string sql = "";
            //如果转换失败
            if (int.TryParse(domain, out outManuId))
            {
                sql = string.Format($@"SELECT name FROM tb_user WHERE manufacturer_id={outManuId} AND system_role_id=-10");
                var model = SQLHelper.Query<tb_manu>(sql);
                domain = model.Name;
            }
            return domain;

        }
        private string GetUrlHost()
        {
            var txtUrl = txt3.Text.Trim();
            if (string.IsNullOrEmpty(txtUrl))
            {
                var domain = GetDomain();
                return domain + ".onlyid.cn";
            }
            return txtUrl;
        }
        private string GetDateTime()
        {
            var date = txt2.Text.Trim();
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.ToString("yyyyMMdd");
            }
            return date;
        }

     
    }
}
