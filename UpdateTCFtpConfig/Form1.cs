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
using Aaron.Common;
using Models;

namespace UpdateTCFtpConfig
{
    public partial class Form1 : Form
    {

        public static List<string> list;
        public static List<Button> bList;
        IList<tb_manu> models = null;
        private int max = 5;
        public Form1()
        {
            InitializeComponent();
            txt1.TabIndex = 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //加载右边的按钮
            string listJson = XMLHelper.GetNodeText(XMLPath.OldFtp);
            list = JsonHelper.DeserializeObject<List<string>>(listJson);
            bList = new List<Button>() { btn1, btn2, btn3, btn4, btn5 };

            //初始化右边五个按键的字
            string sql = string.Format($@"SELECT  a.tb_manufacturerID,a.name,b.name AS domain FROM dbo.tb_manufacturer a LEFT JOIN tb_user b ON a.tb_manufacturerID=b.manufacturer_id WHERE tb_manufacturerID in({string.Join(",", list)}) AND b.system_role_id=-10");

            for (int i = 0; i < list.Count; i++)
            {
                bList[i].Text = list[i] + $"(临时按键名称)";
            }
        }
        private void BtnClick(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var maunId = btn.Text.GetFirstInt().Trim();
            var domain = models.First(c => c.tb_manufacturerID == maunId).Domain;
            SetFtp(domain);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newManuName = this.txt1.Text.Trim();
            SetFtp(newManuName);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetFtpConfig(true);
        }


        private void SetFtp(string newManuName)
        {
            //先重置为原来的
            ResetFtpConfig(false);

            //重新设置
            string path = XMLHelper.GetNodeText(XMLPath.Ftp);
            List<string> lines = new List<string>(File.ReadAllLines(path, Encoding.Default));
            //可以传入manuId或者直接中文
            int outManuId = 0;
            string manuId = "10086"; ;

            for (int i = 0; i < lines.Count; i++)
            {
                var item = lines[i];
                if (item.StartsWith("directory") || item.StartsWith("localdir"))
                {
                    if (!item.Contains("/bin"))
                    {

                        if (item.Contains("/Areas"))
                        {
                            //针对一码通
                            string dicYmt = @"/Areas";
                            int indexYmt = item.IndexOf(dicYmt);
                            string newName = dicYmt + dicYmt[0] + newManuName;
                            item = item.Remove(indexYmt);
                            item = item.Insert(indexYmt, newName);
                        }
                        else
                        {
                            string dic1 = @"\custom";
                            string dic2 = @"/custom";
                            int index = item.IndexOf(dic1);
                            int index2 = item.IndexOf(dic2);
                            string newName = "";
                            if (index > 0 || index2 > 0)
                            {
                                if (index > 0)
                                {
                                    newName = dic1 + dic1[0] + newManuName;

                                }
                                else
                                {
                                    newName = dic2 + dic2[0] + newManuName;
                                    index = index2;
                                }
                                item = item.Remove(index);
                                item = item.Insert(index, newName);
                            }
                        }
                    }
                }
                lines[i] = item;
            }

            File.WriteAllLines(path, lines.ToArray(), Encoding.Default);

            //插入历史记录
            //如果已经存在            
            if (list.Contains(manuId))
            {
                var indexAt = list.Remove(manuId);
            }

            if (list.Count() == max)
            {
                list.RemoveAt(0);
                list.Add(manuId);
            }
            else
            {
                list.Add(manuId);
            }
            UpdateXML();
            MessageBox.Show("修改成功");
            this.Close();
        }
        private void ResetFtpConfig(bool isShow)
        {
            string path = XMLHelper.GetNodeText(XMLPath.Ftp);
            List<string> lines = new List<string>(File.ReadAllLines(path, Encoding.Default));
            string newManuName = this.txt1.Text;
            for (int i = 0; i < lines.Count; i++)
            {
                var item = lines[i];
                if (item.StartsWith("directory") || item.StartsWith("localdir"))
                {
                    if (!item.Contains("/bin"))
                    {
                        if (item.Contains("/Areas"))
                        {
                            //针对一码通
                            string dicYmt = @"/Areas";
                            int indexYmt = item.IndexOf(dicYmt);
                            item = RemoveStr(dicYmt, item, indexYmt);
                        }
                        else
                        {
                            string dic1 = @"\custom";
                            string dic2 = @"/custom";
                            int index = item.IndexOf(dic1);
                            int index2 = item.IndexOf(dic2);

                            if (index > 0)
                            {
                                item = RemoveStr(dic1, item, index);
                            }
                            else if (index2 > 0)
                            {
                                item = RemoveStr(dic2, item, index2);
                            }
                        }
                    }
                }
                lines[i] = item;
            }

            File.WriteAllLines(path, lines.ToArray(), Encoding.Default);
            if (isShow)
            {
                MessageBox.Show("重置成功");
            }
        }
        private string RemoveStr(string dic, string item, int index)
        {
            var removeIndex = index + dic.Length;
            if (item.Length > removeIndex)
            {
                item = item.Remove(removeIndex);
            }
            return item;
        }

        private void UpdateXML()
        {
            var jsonStr = JsonHelper.SerializeObject(list);
            XMLHelper.UpdateNodeInnerText(XMLPath.OldFtp, jsonStr);
        }

    }
}
