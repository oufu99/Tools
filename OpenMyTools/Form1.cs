﻿using Common;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OpenSoft(string path)
        {
            //如果这个解决方案是重新拉取的  要先整个解决方案生成一遍
            System.Diagnostics.Process.Start(path);
            this.Close();
        }

        private void updateFtpBtn_Click(object sender, EventArgs e)
        {
            OpenSoft(XMLHelper.GetPath(XMLPath.UpdateTCFtpConfigExe));
        }

        private void queryCodeBtn_Click(object sender, EventArgs e)
        {
            OpenSoft(XMLHelper.GetPath(XMLPath.QueryCodeExe));
        }

        private void addManuBtn_Click(object sender, EventArgs e)
        {
            OpenSoft(XMLHelper.GetPath(XMLPath.AddNewManuExe));
        }

        private void updateShortCodeBtn_Click(object sender, EventArgs e)
        {
            OpenSoft(XMLHelper.GetPath(XMLPath.UpdateOneLineExe));
        }

        private void updateAllShortCodeBtn_Click(object sender, EventArgs e)
        {
            OpenSoft(XMLHelper.GetPath(XMLPath.UpdateShortCodeExe));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenSoft(XMLHelper.GetPath(XMLPath.OpenMyTools));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenSoft(XMLHelper.GetPath(XMLPath.CheckLogExe));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenSoft(XMLHelper.GetPath(XMLPath.WsBuildExe));
        }
    }
}
