using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenMyTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OpenSoft(string path)
        {
            //如果这个解决方案是重新拉取的  要先整个解决方案生成一遍
            Process.Start(path);
            this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("calc.exe");
        }
    }
}
