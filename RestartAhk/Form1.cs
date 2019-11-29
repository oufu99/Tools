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

namespace RestartAhk
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RestartAllAhk();
        }

        private void RestartAllAhk()
        {
            var ahkPath = ConfigHelper.GetAppConfig("AhkPath");
            var list = ahkPath.Split(new string[] { @";" }, StringSplitOptions.RemoveEmptyEntries);
            var killList = new List<string>() { "AutoHotkey", "Vimd" };

            ProcessHelper.KillProgramByList(killList);
            foreach (var item in list)
            {
                FileHelper.OpenSoft(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RestartAllAhk();
        }
    }
}
