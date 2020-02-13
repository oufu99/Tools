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
    /// <summary>
    /// 此工具在文件FileNames.txt 中添加路径或文件夹,可以复制到target的路径中去 也可以直接还原
    /// </summary>
    public partial class Form1 : Form
    {
        string targetBasePath = ConfigHelper.GetAppConfig("TargetDic");

        public Form1()
        {
            InitializeComponent();
            //OpenConfigText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenConfigText();
        }

        private void OpenConfigText()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileNames.txt");
            //要更新的项目
            FileHelper.OpenSoft(filePath);
        }

        private void CopyGit(bool isCopy)
        {

            //创建目标文件夹
            FileHelper.CreateDirectory(targetBasePath);

            //复制
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileNames.txt");
            var fileStr = File.ReadAllLines(filePath);
            foreach (var item in fileStr)
            {
                if (string.IsNullOrEmpty(item.Trim()))
                {
                    continue;
                }
                var forString = item;
                var itemFilePath = forString.Trim();
                //如果是文件夹
                if (FileHelper.CheckIsFolder(itemFilePath))
                {
                    //目标文件夹
                    var targetFolderPath = GetFolderTargetPath(itemFilePath);
                    if (isCopy)
                    {
                        FileHelper.CopyDirectory(itemFilePath, targetFolderPath);
                    }
                    else
                    {
                        //如果是还原直接把位置换一下就可以了
                        FileHelper.CopyDirectory(targetFolderPath, itemFilePath);
                    }
                    continue;
                }

                //判断要复制过去的位置文件夹
                var targetFileFolderPath = GetFileTargetPath(itemFilePath);
                FileHelper.CreateDirectory(targetFileFolderPath);
                var fileName = FileHelper.GetFileNameByFullPath(itemFilePath);
                var targetFilePath = Path.Combine(targetFileFolderPath, fileName);

                if (isCopy)
                {
                    FileHelper.CopyFile(itemFilePath, targetFilePath, true);
                }
                else
                {
                    //还原,直接把两个路径改一下就还原回去了
                    FileHelper.CopyFile(targetFilePath, itemFilePath, true);
                }
            }

        }

        private string GetFolderTargetPath(string fullPath)
        {
            var sourceTarget = FileHelper.GetFullNamenNotDiskByFullPath(fullPath);
            var targetFinallyPath = Path.Combine(targetBasePath, sourceTarget);
            return targetFinallyPath;
        }

        private string GetFileTargetPath(string fullPath)
        {
            var middleName = FileHelper.GetMiddleNameByFullPath(fullPath);
            var targetFinallyPath = Path.Combine(targetBasePath, middleName);
            return targetFinallyPath;

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

        private void button4_Click(object sender, EventArgs e)
        {
            var binFolder = AppDomain.CurrentDomain.BaseDirectory;
            var tempfilePath = Path.Combine(binFolder, "FileNames.txt");
            //正式环境的路径
            var targetPath = FileHelper.GetParentPath(tempfilePath, 3,true);
            FileHelper.CopyFile(tempfilePath, targetPath);
        }
    }
}
