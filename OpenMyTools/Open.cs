﻿using Aaron.Common;
using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenMyTools
{
    public partial class Open : Form
    {

        Dictionary<string, string> ProgramDic = new Dictionary<string, string>();
        string filePath = "";
        List<Button> BtnList = new List<Button>();
        string xmlPath = @"D:\Tools\Common\config.config";
        string classPath = @"D:\Tools\Common\XMLPath.cs";
        string classProjectPath = @"D:\Tools\Common\Common.csproj";

        public Open()
        {
            InitializeComponent();
            filePath = GetProjectPathByBasePath(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //要更新的项目
            var fileStr = File.ReadAllLines(filePath);
            foreach (var item in fileStr)
            {
                if (string.IsNullOrEmpty(item))
                {
                    break;
                }
                var arr = item.Split(new string[] { @";" }, StringSplitOptions.RemoveEmptyEntries);
                ProgramDic.Add(arr[0], arr[1]);
            }

            //自动生成checkBox控件
            int leftX = this.postionButton.Location.X;
            int leftY = this.postionButton.Location.Y;
            int rowCount = 0;
            int hangCount = 1;
            //生成左边的 
            foreach (KeyValuePair<string, string> keyValuePair in ProgramDic)
            {
                var leftXNew = 0;
                var leftYNew = 0;
                if (rowCount == 0)
                {
                    leftXNew = leftX + (rowCount * 30);
                }
                else
                {
                    leftXNew = leftX + (rowCount * (this.postionButton.Width + 30));
                }

                //Y 轴
                leftYNew = leftY + (hangCount * (this.postionButton.Height + 20));

                Button btn = new Button();
                btn.Text = keyValuePair.Key;
                btn.Name = keyValuePair.Value;
                btn.Location = new Point(leftXNew, leftYNew);
                btn.Width = this.postionButton.Width;
                btn.Height = this.postionButton.Height;
                btn.Click += Btn_Click;
                this.Controls.Add(btn);
                BtnList.Add(btn);
                rowCount++;
                if (rowCount == 5)
                {
                    rowCount = 0;
                    hangCount++;
                }
            }
        }
        private string GetProjectPathByBasePath(string path)
        {
            return Path.Combine(path, "ProjectPath.txt");

        }
        private void button1_Click(object sender, EventArgs e)
        {

            //上面放到保存的位置就可以了
            if (!this.textBox1.Visible)
            {
                this.textBox1.Visible = true;
                this.textBox1.Focus();
                BtnList.ForEach(c => c.Visible = false);
                this.postionButton.Text = "保存";
            }
            //添加新项目
            else
            {
                var text = this.textBox1.Text;
                if (!string.IsNullOrEmpty(text))
                {
                    //保存
                    var basePath = FileHelper.GetParentPath(filePath, 3);
                    var trueFile = GetProjectPathByBasePath(basePath);
                    //更新bin目录和主目录的ProjectPath  这里要换行是因为没有用List来载入,所以要换行才会开始重新一行
                    //File.AppendAllText(filePath, "\n" + text);
                    File.AppendAllText(trueFile, "\n" + text);

                    //更新XML
                    var strList = File.ReadAllLines(xmlPath).ToList();
                    for (int i = 0; i < strList.Count; i++)
                    {
                        if (strList[i].Contains(@"<!--快捷程序路径结束-->"))
                        {
                            var str = text.Split(new string[] { @";" }, StringSplitOptions.RemoveEmptyEntries);
                            var key = str[1];
                            var itemPath = key.Substring(0, key.Count() - 3);
                            var xmlLine = FormaterXMLLine(key, itemPath);
                            strList.Insert(i, xmlLine);
                            i++;
                        }
                    }
                    File.WriteAllLines(filePath, strList);

                    //更新类
                    var classList = File.ReadAllLines(classPath).ToList();
                    for (int i = 0; i < classList.Count; i++)
                    {
                        if (classList[i].Contains(@"//所有程序路径结束"))
                        {
                            var str = text.Split(new string[] { @";" }, StringSplitOptions.RemoveEmptyEntries);
                            var key = str[1];
                            //Count-3 是为了去掉Exe这三个字母
                            var itemPath = key.Substring(0, key.Count() - 3);
                            //有空格用来占位  相当于格式化代码
                            var classLine = $"        public const string {key} = \"configuration/{key}\";";
                            classList.Insert(i, classLine);
                            i++;
                        }
                    }
                    File.WriteAllLines(classPath, classList);
                    AutoBuildHelper.BuildOutBin(classProjectPath);
                }

                //把按钮还原回去
                this.textBox1.Visible = false;
                this.postionButton.Text = "添加子项目";
                BtnList.ForEach(c => c.Visible = true);
            }

        }



        private string FormaterXMLLine(string key, string dir)
        {
            //<OpenBatchSoftwareExe><![CDATA[D:\Tools\OpenBatchSoftware\bin\Debug\OpenBatchSoftware.exe]]></OpenBatchSoftwareExe>
            var res = $@"  <{key}><![CDATA[D:\Tools\{dir}\bin\Debug\{dir}.exe]]></{key}>";
            return res;
        }



        /// <summary>
        /// 用反射的方法,然后btn填成那个名字,就可以用统一的方法来调用了
        /// 通用方法,在XMLpath中和config文件中添加属性  再把btn的Name改成和xmlpath中一样,就能自动反射了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Click(object sender, EventArgs e)
        {
            //通过反射来获取值,然后调用OpenSoft方法
            var btn = (Button)sender;
            var propertyName = btn.Name;
            Assembly ass = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "Common.dll");
            string className = "Common.XMLPath";
            Type t = ass.GetType(className);
            var path = t.GetField(propertyName).GetValue(null).ToString().Trim();
            FileHelper.OpenSoft(XMLHelper.GetNodeText(path).Trim());
        }


        /// <summary>
        /// 重启项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //打开新项目,用来重启这个项目
            FileHelper.ReloadSoft("OpenMyTools");
            this.Close();
        }
    }
}
