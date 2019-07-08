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

namespace ConvertMusicInfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //读取音乐文件,修改歌手信息

            var path = @"D:\认知方法论-音频\bf0421.mp3";
            FileInfo file = = new FileInfo(fullPath);


        }
    }
}
