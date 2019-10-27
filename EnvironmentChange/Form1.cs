using Aaron.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvironmentChange
{
    public partial class Form1 : Form
    {

        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenSoft();
        }

        private void OpenSoft()
        {
            var openList = new List<string>()
            {
                @"D:\MyLoove\TotalCommand\VimD\vimd.exe",
                @"D:\MyLoove\TotalCommand\VimD\userPlugins\InitProgram.ahk"
            };

            foreach (var item in openList)
            {
                FileHelper.OpenSoft(item);
            }

        }

        private void CloseSoft()
        {
            var delList = new List<string>()
            {
                "autohotkey",
                "vimd"
            };
            //关闭ahk
            var ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                if (delList.Contains(p.ProcessName.ToLower()))
                {
                    p.Kill();
                    p.WaitForExit();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CloseSoft();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CloseSoft();
            FileHelper.OpenSoft(@"F:\Game\5211game\11Loader.exe");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CloseSoft();
            FileHelper.OpenSoft(@"F:\Game\dzclient\Platform.exe");
        }
    }
}
