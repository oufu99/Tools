using Common;
using Models;
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


namespace UpdateShortCode
{
    public partial class Form1 : Form
    {
        public static List<string> list;
        public static List<Button> bList;

        private int max = 5;
        public Form1()
        {
            InitializeComponent();

            string listJson = XMLHelper.GetPath(XMLPath.OldMadnuId);
            list = JsonHelper.DeserializeObject<List<string>>(listJson);
            bList = new List<Button>() { btn1, btn2, btn3, btn4, btn5 };

            //初始化右边五个按键的字
            string sql = string.Format($@"SELECT  a.tb_manufacturerID,a.name,b.name AS domain FROM dbo.tb_manufacturer a LEFT JOIN tb_user b ON a.tb_manufacturerID=b.manufacturer_id WHERE tb_manufacturerID in({string.Join(",", list)}) AND b.system_role_id=-10");
            var models = SQLHelper.QueryList<tb_manu>(sql);
            for (int i = 0; i < list.Count; i++)
            {
                bList[i].Text = list[i] + $"({models.First(c => c.tb_manufacturerID == list[i]).Name})";
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate_Click();
        }

        private void btnUpdate_Click()
        {
            var newManuId = this.txt1.Text;
            UpdateAllShortCut(newManuId);
        }
        private void UpdateXML()
        {
            var jsonStr = JsonHelper.SerializeObject(list);
            XMLHelper.UpdateNodeInnerText(XMLPath.OldMadnuId, jsonStr);
        }

        private void BtnClick(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var maunId = btn.Text.GetFirstInt();
            UpdateAllShortCut(maunId);
        }

        private void UpdateAllShortCut(string newManuId)
        {

            var manuId = list.Last();
            string path = XMLHelper.GetPath(XMLPath.SQLShortCut);
            var files = Directory.GetFiles(path, "*.sqlpromptsnippet");
            foreach (var file in files)
            {
                string text = File.ReadAllText(file);
                string newText = text.Replace(manuId, newManuId);
                File.WriteAllText(file, newText, Encoding.UTF8);
            }
            //如果已经存在
            if (list.Contains(newManuId))
            {
                var indexAt = list.Remove(newManuId);
            }

            if (list.Count() == max)
            {
                list.RemoveAt(0);
                list.Add(newManuId);
            }
            else
            {
                list.Add(newManuId);
            }
            UpdateXML();
            this.Close();
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
                btnUpdate_Click();//触发按钮事件
                return true;
            }
            if (keyData == Keys.Escape)//Esc退出键
            {
                this.Close();
                return true;
            }
            return false;
        }

       
    }
}
