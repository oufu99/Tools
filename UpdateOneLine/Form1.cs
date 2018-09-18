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
using Common;

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
        public Form1()
        {
            InitializeComponent();
            //获取焦点
            txt1.TabIndex = 0;

        }

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
            string path = XMLHelper.GetPath(XMLPath.SQLShortCut);
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
                doc.Load(XMLHelper.GetPath(XMLPath.StandardSQLShortCut));
                XmlElement rootElem = doc.DocumentElement;

                //设置
                var shortCutNode = rootElem.GetElementsByTagName("Shortcut")[0]; //获取person子节点集合
                shortCutNode.InnerText = shortCut;
                var titleNode = rootElem.GetElementsByTagName("Title")[0]; //获取person子节点集合
                titleNode.InnerText = shortCut;

                var codeNode = rootElem.GetElementsByTagName("Code")[0]; //获取person子节点集合
                codeNode.InnerText = content;

                var newText = ConvertXmlToString(doc);
                var newFileName = string.Format($@"{XMLHelper.GetPath(XMLPath.SQLShortCut)}\{shortCut}.sqlpromptsnippet");
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


    }
}
