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
            //关闭ahk

            var ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                if (p.ProcessName.Contains("AutoHotkey"))
                {
                    
                    p.Kill();
                    p.WaitForExit(); // possibly with a timeout
                     
                }
            }


        }
    }
}
