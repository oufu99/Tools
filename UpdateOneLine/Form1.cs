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
using System.Xml;
using System.Xml.Linq;
using Aaron.Common;

namespace UpdateOneLine
{
    public partial class Form1 : Form
    {
        public static Action methodCall;
        public static string targetFileName;
        public static XmlDocument doc;

        public static XmlNode nameNode;

        //用来标记是修改还是新增  不点击那个读取按钮就是0
        public static int changeFlag = 0;

        RichTextBox richTextBox = null;
        public Form1()
        {
            InitializeComponent();
            //获取焦点
            txt1.TabIndex = 0;

            //自动生成一个按钮放在保存的下面
            //var y = this.button1.Location.Y;
            //var x = this.button1.Location.X;
            //Button btn = new Button();
            //btn.Text = "动态生成";
            //btn.Location = new Point(x, y + 30);
            //btn.Click += manuBtn_Click;
            //this.Controls.Add(btn);
        }


        private void manuBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show(e.ToString());
        }
     

        #region 文本框改变事件
        private void txt1_TextChanged(object sender, EventArgs e)
        {
            //测试的时候直接new  后面改的时候再把这句话注释掉
            richTextBox = new RichTextBox();
            //根据输入查出来类似的然后每一行添加到这个TextBox中去
            if (richTextBox != null)
            {

            }
            else
            {
                richTextBox = new RichTextBox();
                richTextBox.Name = "richTextBox";

                var pointX = txt1.Location.X;
                var pointY = txt1.Location.Y;
                var height = txt1.Height;
                richTextBox.Location = new Point(pointX, pointY + height);
                richTextBox.BringToFront();
                //将groubox添加到页面上
                this.Controls.Add(richTextBox);
                this.Controls.SetChildIndex(richTextBox, 0);
            }


        }

        private string GetContainStr()
        {

            return "";
        }

        private void txt1_Leave(object sender, EventArgs e)
        {
            //    Controls.Remove(richTextBox);
            //    richTextBox = null;
        }
        #endregion



        private void btn1_Click(object sender, EventArgs e)
        {
            SelctClick();
        }

        private void SelctClick()
        {
            //进来了就说明是更新
            changeFlag = 1;
            var name = txt1.Text;
            name = string.IsNullOrEmpty(name) ? "scust" : name;
            string path = XMLHelper.GetNodeText(XMLPath.SQLShortCut);
            var files = Directory.GetFiles(path, "*.sqlpromptsnippet");
            targetFileName = files.First(c => c.Contains(name));
            if (string.IsNullOrEmpty(targetFileName))
            {
                return;
            }
            doc = new XmlDocument();
            doc.Load(targetFileName);
            XmlElement rootElem = doc.DocumentElement;
            nameNode = rootElem.GetElementsByTagName("Code")[0]; //获取person子节点集合
            string text = nameNode.InnerText;
            richTextBox1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModifyClick();
        }

        
        private void ModifyClick()
        {
            if (changeFlag == 1)
            {
                var text = richTextBox1.Text;
                nameNode.InnerText = text;
                var newText = ConvertXmlToString(doc);
                File.WriteAllText(targetFileName, newText);
                MessageBox.Show("修改成功");
            }
            else
            {
                var shortCut = txt1.Text;
                var content = richTextBox1.Text;
                doc = new XmlDocument();
                doc.Load(XMLHelper.GetNodeText(XMLPath.StandardSQLShortCut));
                XmlElement rootElem = doc.DocumentElement;

                //设置
                var shortCutNode = rootElem.GetElementsByTagName("Shortcut")[0]; //获取person子节点集合
                shortCutNode.InnerText = shortCut;
                var titleNode = rootElem.GetElementsByTagName("Title")[0]; //获取person子节点集合
                titleNode.InnerText = shortCut;

                var codeNode = rootElem.GetElementsByTagName("Code")[0]; //获取person子节点集合
                codeNode.InnerText = content;

                var newText = ConvertXmlToString(doc);
                var newFileName = string.Format($@"{XMLHelper.GetNodeText(XMLPath.SQLShortCut)}\{shortCut}.sqlpromptsnippet");
                File.WriteAllText(newFileName, newText);
                MessageBox.Show("新增成功");
            }
        }

        /// <summary>
        /// 将XmlDocument转化为string
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public string ConvertXmlToString(XmlDocument xmlDoc)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return xmlString;
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
                SelctClick();//触发按钮事件
                return true;
            }
            if (keyData == Keys.Escape)//Esc退出键
            {
                this.Close();
                return true;
            }
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
