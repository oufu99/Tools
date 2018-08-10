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

namespace UpdateOneLine
{
    public partial class Form1 : Form
    {

        public static string targetFileName;
        public static XmlDocument doc;

        public static XmlNode nameNode;
        public Form1()
        {
            InitializeComponent();
            //获取焦点
            txt1.TabIndex = 0;
           
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            var name = txt1.Text;
            name = string.IsNullOrEmpty(name) ? "scust" : name;
            string path = @"C:\Users\Administrator\AppData\Local\Red Gate\SQL Prompt 7\Snippets";
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
            textArea1.Text = text;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var text = textArea1.Text;
            nameNode.InnerText = text;
            var newText = ConvertXmlToString(doc);
            File.WriteAllText(targetFileName, newText);

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

    }
}
