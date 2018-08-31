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

namespace AddNewManu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.txt1.TabIndex = 0;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            //选择文件,然后复制
            string filePath = "";
            string fileName = "";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件|*.*"; //设置要选择的文件的类/设置要选择的型
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = fileDialog.FileName;//返回文件的完整路径     
                fileName = fileDialog.SafeFileName;

            }
            string name = this.txt1.Text;
            string path = @"D:\MyConfig\我做过的项目\" + name;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                string file1 = $@"{path}\{name}.sql";
                File.Create(file1);
                string file2 = $@"{path}\{name}url.txt";
                File.Create(file2);
                string file3 = $@"{path}\{fileName}";
                File.Move(filePath, file3);
            }
            MessageBox.Show("创建成功");
            this.Close();
        }
    }
}
