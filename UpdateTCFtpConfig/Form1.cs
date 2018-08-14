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
            string path = @"C:\MyLove\TotalCommand\wcx_ftp.ini";

            List<string> lines = new List<string>(File.ReadAllLines(path, Encoding.Default));
            string newManuName = this.txt1.Text;
            for (int i = 0; i < lines.Count; i++)
            {
                var item = lines[i];
                if (item.StartsWith("directory"))
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
            string path = @"C:\MyLove\TotalCommand\wcx_ftp.ini";

            List<string> lines = new List<string>(File.ReadAllLines(path, Encoding.Default));
            string newManuName = this.txt1.Text;
            for (int i = 0; i < lines.Count; i++)
            {
                var item = lines[i];
                if (item.StartsWith("directory"))
                {
                    if (!item.Contains("/bin"))
                    {

                        if (item.Contains("/Areas"))
                        {
                            //针对一码通
                            string dicYmt = @"/Areas";
                            int indexYmt = item.IndexOf(dicYmt);
                            item= RemoveStr(dicYmt,item, indexYmt);
                            //var removeIndex = indexYmt + dicYmt.Length;
                            //if (item.Length > removeIndex)
                            //{
                            //    item = item.Remove(removeIndex);
                            //}
                        }
                        else
                        {
                            string dic1 = @"\custom";
                            string dic2 = @"/custom";
                            int index = item.IndexOf(dic1);
                            int index2 = item.IndexOf(dic2);

                            if (index > 0)
                            {
                                //var removeIndex = index + dic1.Length;
                                //if (item.Length > removeIndex)
                                //{
                                //    item = item.Remove(removeIndex);
                                //}
                                item = RemoveStr(dic1, item, index);
                            }
                            else if (index2 > 0)
                            {
                                //var removeIndex = index2 + dic2.Length;
                                //if (item.Length > removeIndex)
                                //{
                                //    item = item.Remove(removeIndex);
                                //}
                                item = RemoveStr(dic2, item, index2);
                            }
                        }
                    }
                }
                lines[i] = item;
            }

            File.WriteAllLines(path, lines.ToArray(), Encoding.Default);
            MessageBox.Show("重置成功");
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
