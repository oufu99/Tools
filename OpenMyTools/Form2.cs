using Aaron.Common;
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
    public partial class Form2 : Form
    {

        Dictionary<string, string> ProgramDic = new Dictionary<string, string>();
        string filePath = "";
        List<Button> BtnList = new List<Button>();
        public Form2()
        {
            InitializeComponent();
            filePath = GetProjectPathByBasePath(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //添加一个dic  用来遍历添加按钮


            //要更新的项目
            var fileStr = File.ReadAllLines(filePath);
            foreach (var item in fileStr)
            {
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
           // button1_Click(null, null);
        }
        private string GetProjectPathByBasePath(string path)
        {
            return Path.Combine(path, "ProjectPath.txt");

        }
        private void button1_Click(object sender, EventArgs e)
        {
            var text = "批量打开软件2;PpenBatchSoftwareExe";
            var basePath = GetParentPath(filePath, 3);
            var trueFile = GetProjectPathByBasePath(basePath);
            //保存到打开Tool的里面去
            //System.IO.File.AppendAllText(filePath, text);
            //FileHelper.MoveFile(filePath, trueFile, true);

            var xmlPath = @"D:\Tools\Common\config.config";
            var strList = System.IO.File.ReadAllLines(xmlPath).ToList();
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
            //更新类

            string classPath = @"D:\Tools\Common\XMLPath.cs";
            var classList = System.IO.File.ReadAllLines(classPath).ToList();
            for (int i = 0; i < classList.Count; i++)
            {
                if (classList[i].Contains(@"<!--快捷程序路径结束-->"))
                {

                    var str = text.Split(new string[] { @";" }, StringSplitOptions.RemoveEmptyEntries);
                    var key = str[1];
                    var itemPath = key.Substring(0, key.Count() - 3);
                    var classLine = $"public const string {key} = \"configuration/{key}\";";
                    classList.Insert(i, classLine);
                    i++;
                }
            }

            //重新编译Common



            return;
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
                var newItem = this.textBox1.Text;
                if (!string.IsNullOrEmpty(newItem))
                {
                    //保存





                }

                //把按钮还原回去
                this.textBox1.Visible = false;
                this.postionButton.Text = "添加子项目";
                BtnList.ForEach(c => c.Visible = true);
            }



            //打开bin目录的txt 更新以后覆盖原来的


            //然后找到占位符,在结束的上面添加一行数据


            //文件里面只管相对路径,  基础路径写在Helper里面
        }



        private string FormaterXMLLine(string key, string dir)
        {
            //<OpenBatchSoftwareExe><![CDATA[D:\Tools\OpenBatchSoftware\bin\Debug\OpenBatchSoftware.exe]]></OpenBatchSoftwareExe>
            var res = $@"<{key}><![CDATA[D:\Tools\{dir}\bin\Debug\{dir}.exe]]></{key}>";
            return res;
        }

        /// <summary>
        /// 根据传入的路径 获取向前几层
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dirCount"></param>
        /// <returns></returns>
        public static string GetParentPath(string path, int dirCount)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }
            var temp = path.Replace(@"/", @"\");
            var list = new List<int>();
            var index = 0;
            while (index != -1)
            {
                var tempIndex = index + 1;
                index = temp.IndexOf(@"\", tempIndex);
                if (index > 0)
                {
                    list.Add(index);
                }
            }
            var res = temp.Substring(0, list[list.Count - dirCount]);
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
            OpenSoft(XMLHelper.GetNodeText(path).Trim());
        }

        //打开系统的计算器

        private void OpenCalc(object sender, EventArgs e)
        {
            Process.Start("calc.exe");
        }

        private void OpenSoft(string path)
        {
            //如果这个解决方案是重新拉取的  要先整个解决方案生成一遍
            Process.Start(path);
            this.Close();
        }
    }
}
