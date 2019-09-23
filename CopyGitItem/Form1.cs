using Aaron.Common;
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

namespace CopyGitItem
{
    public partial class Form1 : Form
    {
        string targetBasePath = @"D:\CopyGitItem";
        string sourceBasePath = @"E:\ZPCode\zp.ymt";
        public Form1()
        {
            InitializeComponent();
            OpenGitTxt();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenGitTxt();
        }

        private void OpenGitTxt()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GitItem.txt");
            //要更新的项目
            FileHelper.OpenSoft(filePath);
        }

        private void CopyGit(bool isCopy)
        {

            //创建目标文件夹
            if (isCopy)
            {
                FileHelper.CleanDirectory(targetBasePath);
            }
            var list = new List<string>();
            //复制
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GitItem.txt");
            var fileStr = File.ReadAllLines(filePath);
            foreach (var item in fileStr)
            {
                if (string.IsNullOrEmpty(item))
                {
                    break;
                }
                var forString = item;
                var itemFilePath = forString.Trim();
                itemFilePath = Path.Combine(sourceBasePath, itemFilePath);
                var fileName = FileHelper.GetFileNameByFullPath(itemFilePath);
                //分开不同的文件夹保存
                var dicList = forString.Split(new string[] { @"/" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                //因为最后一个是文件名   所以移除最后一个 
                dicList.Remove(dicList.Last());
                var combinPath = targetBasePath;
                foreach (var dirItem in dicList)
                {
                    //清除格式
                    var formatterItem = dirItem.Replace("\t", "").Replace("\n", "").Trim();
                    //按文件夹单独放置
                    combinPath = Path.Combine(combinPath, formatterItem);
                    FileHelper.CreateDirectory(combinPath);
                }
                fileName = Path.Combine(combinPath, fileName);
                if (isCopy)
                {
                    FileHelper.MoveFile(itemFilePath, fileName, true);
                }
                else
                {
                    //还原,直接把两个路径改一下就还原回去了
                    FileHelper.MoveFile(fileName, itemFilePath, true);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CopyGit(true);
            MessageBox.Show("复制完成");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //还原
            CopyGit(false);
            MessageBox.Show("还原完成");
        }
    }
}
