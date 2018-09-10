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
using Common;
using Models;

namespace UpdateTCFtpConfig
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txt1.TabIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //先重置为原来的
            ResetFtpConfig(false);

            //重新设置
            string path = XMLHelper.GetPath(XMLPath.Ftp);
            List<string> lines = new List<string>(File.ReadAllLines(path, Encoding.Default));
            string newManuName = this.txt1.Text;
            //可以传入manuId或者直接中文
            int manuId = 0;
            if (int.TryParse(newManuName, out manuId))
            {
                string sql = string.Format($@"SELECT a.name,b.name AS domain FROM dbo.tb_manufacturer a LEFT JOIN tb_user b ON a.tb_manufacturerID=b.manufacturer_id WHERE tb_manufacturerID={manuId} AND b.system_role_id=-10");
                var model = SQLHelper.Query<tb_manu>(sql);
                if (model == null)
                {
                    MessageBox.Show("厂商不存在!");
                    return; 
                }
                newManuName = model.Domain;
            }
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
            MessageBox.Show("修改成功");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetFtpConfig(true);
        }
        private void ResetFtpConfig(bool isShow)
        {
            string path = XMLHelper.GetPath(XMLPath.Ftp);
            List<string> lines = new List<string>(File.ReadAllLines(path, Encoding.Default));
            string newManuName = this.txt1.Text;
            for (int i = 0; i < lines.Count; i++)
            {
                var item = lines[i];
                if (item.StartsWith("directory")||item.StartsWith("localdir"))
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
    }
}
