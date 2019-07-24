using Aaron.Common;
using Common;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        //navicat列表
        public static List<string> nlist;
        private int max = 5;
        public Form1()
        {
            InitializeComponent();

            string listJson = XMLHelper.GetNodeText(XMLPath.OldMadnuId);
            list = JsonHelper.DeserializeObject<List<string>>(listJson);
            bList = new List<Button>() { btn1, btn2, btn3, btn4, btn5 };

            //更新navicat的  现在改用了ahk暂时先搁置
            string nListJson = XMLHelper.GetNodeText(XMLPath.NavicatOldManuId);
            nlist = JsonHelper.DeserializeObject<List<string>>(nListJson);
            //初始化右边五个按键的字
            string sql = string.Format($@"SELECT  a.tb_manufacturerID,a.name,b.name AS domain FROM dbo.tb_manufacturer a LEFT JOIN tb_user b ON a.tb_manufacturerID=b.manufacturer_id WHERE tb_manufacturerID in({string.Join(",", list)}) AND b.system_role_id=-10");
            var models = SQLHelper.QueryList<tb_manu>(sql);
            for (int i = 0; i < list.Count; i++)
            {
                bList[i].Text = list[i] + $"({models.First(c => c.tb_manufacturerID == list[i]).Name})";
                bList[i].Click += BtnClick;
            }
            for (int i = list.Count; i < bList.Count; i++)
            {
                bList[i].Hide();
            }
        }

        //代码注释掉,只是为了后面参考  已经改为使用下拉框了
        private void Form1_Load(object sender, EventArgs e)
        {
            #region 生成控件  用来后面参考
            ////初始化两个坐标,后面生成的以这两个为基准
            //int leftX = 0;
            //int leftY = 0;
            //int rigthX = 0;
            //int rigthY = 0;
            ////生成左边的 
            //for (int i = 0; i < list.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        leftX = this.btnWsbgCopy.Location.X;
            //        leftY = this.btnWsbgCopy.Location.Y - 60;

            //        rigthX = this.btnMobileCopy.Location.X;
            //        rigthY = this.btnMobileCopy.Location.Y - 60;
            //    }
            //    else
            //    {
            //        leftY = leftY - 40;
            //        rigthY = rigthY - 40;
            //    }
            //    Button btnLeft = new Button();
            //    btnLeft.Text = list[i];
            //    btnLeft.Location = new Point(leftX, leftY);
            //    btnLeft.Width = this.btnWsbgCopy.Width;
            //    btnLeft.Click += LeftBtnClick;
            //    this.Controls.Add(btnLeft);

            //    Button btnRight = new Button();
            //    btnRight.Text = list[i];
            //    btnRight.Location = new Point(rigthX, rigthY);
            //    btnRight.Width = this.btnWsbgCopy.Width;
            //    btnRight.Click += RightBtnClick;
            //    this.Controls.Add(btnRight); 
            #endregion
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate_Click();
        }

        private void btnUpdate_Click()
        {
            var newManuId = this.txt1.Text.Trim();
            int test = 0;
            if (int.TryParse(newManuId, out test))
            {
                UpdateAllShortCut(newManuId);

            }
            else
            {
                MessageBox.Show("请输入数字");
            }
        }

        private void BtnClick(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var maunId = btn.Text.GetFirstInt().Trim();
            UpdateAllShortCut(maunId);
        }

        private void UpdateAllShortCut(string newManuId)
        {
            var manuId = list.First();

            string path = XMLHelper.GetNodeText(XMLPath.SQLShortCut);
            var files = Directory.GetFiles(path, "*.sqlpromptsnippet");

            var updates = false;
            foreach (var file in files)
            {
                string text = File.ReadAllText(file);
                if (text.Contains(manuId))
                {
                    //用来判断更新是否成功,如果不成功很有可能是最后一次更新的厂商有误,直接提示出来
                    updates = true;
                    string newText = text.Replace(manuId, newManuId);
                    File.WriteAllText(file, newText, Encoding.UTF8);
                }
            }
            if (!updates)
            {
                MessageBox.Show("没有任何一行记录被更新,最后一次更新的厂商是" + manuId + "请查看snippet中的manuId是否正确!");
            }
            //如果已经存在就不处理,不存在就添加
            XMLHelper.UpdateXMLList(list, XMLPath.OldMadnuId, newManuId);


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

        private void button1_Click(object sender, EventArgs e)
        {
            var manuId = nlist.Last();
            var newManuId = this.txt1.Text.Trim();
            string path = XMLHelper.GetNodeText(XMLPath.NavicatShortPath);
            var files = Directory.GetFiles(path, "*.nsnippet");
            foreach (var file in files)
            {
                var isModify = false;
                //改成读取Json
                var texts = File.ReadAllLines(file);
                for (int i = 0; i < texts.Count(); i++)
                {
                    if (texts[i].Contains(manuId))
                    {
                        isModify = true;
                        texts[i] = texts[i].Replace(manuId, newManuId);
                    }
                }
                if (isModify)
                {
                    //没改的就不要修改了
                    File.WriteAllLines(file, texts);
                }
                //文件重命名
                NavicatModel model = JsonHelper.DeserializeObject<NavicatModel>(File.ReadAllText(file));
                var fileName = model.name;

            }
            XMLHelper.UpdateXMLList(nlist, XMLPath.NavicatOldManuId, newManuId);

            //关闭并重启项目
            var pros = Process.GetProcessesByName("navicat");
            pros.First().Kill();
            FileHelper.ReloadSoft("", XMLHelper.GetNodeText(XMLPath.NavicatExe));
            MessageBox.Show("修改成功");
        }
    }
}
