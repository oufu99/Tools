using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenMyTools
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //添加一个dic  用来遍历添加按钮
            var ProgramDic = new Dictionary<string, string>();
            ProgramDic.Add("编译微商代码", "WsBuildExe");
            ProgramDic.Add("复制静态页面", "CopyViewExe");
            





            //自动生成checkBox控件
            int leftX = this.postionButton.Location.X;
            int leftY = this.postionButton.Location.Y;
            int rigthX = 0;
            int rigthY = 0;

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
                this.Controls.Add(btn);

                rowCount++;
                if (rowCount == 5)
                {
                    rowCount = 0;
                    hangCount++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Io打开  
            //D:\Tools\Common\XMLPath.cs
            //D:\Tools\Common\config.config

            //然后找到占位符,在结束的上面添加一行数据


            //文件里面只管相对路径,  基础路径写在Helper里面
        }
    }
}
