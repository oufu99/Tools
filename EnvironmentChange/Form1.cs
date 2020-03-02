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
        public class EnvironmentConfig
        {
            public static List<string> OpenAHKList = new List<string>()
            {
                @"D:\Common\VimD\vimd.exe",
                @"D:\MyLoove\AHK\打开Vimd和Ahk.bat"
            };
            public static List<string> CloseAHKList = new List<string>()
            {
               "autohotkey",
                "vimd"
            };

            public static string GuanFangPath { get; set; } = @"F:\Game\dzclient\Platform.exe";

            /// <summary>
            /// 关闭软件时用的,全部小写因为我用了toLower
            /// </summary>
            public static List<string> CloseSoftList = new List<string>()
                {
                    "platform"
                };
        }

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CloseSoft();
            CloseAHK();
            OpenAHK();
        }

        private void OpenAHK()
        {
            foreach (var item in EnvironmentConfig.OpenAHKList)
            {
                FileHelper.OpenSoft(item);
            }

        }

        private void CloseAHK()
        {
            //关闭ahk
            var ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                if (EnvironmentConfig.CloseAHKList.Contains(p.ProcessName.ToLower()))
                {
                    p.Kill();
                    p.WaitForExit();
                }
            }
        }

        private void CloseSoft()
        {
            var ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                if (EnvironmentConfig.CloseSoftList.Contains(p.ProcessName.ToLower()))
                {
                    p.Kill();
                    p.WaitForExit();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CloseAHK();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            CloseAHK();
            FileHelper.OpenSoft(EnvironmentConfig.GuanFangPath);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            CloseAHK();
            OpenAHK();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CloseAHK();
        }
    }
}
