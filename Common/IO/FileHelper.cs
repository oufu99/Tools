using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IO
{
    public class FileHelper
    {
        public static void CopyDirectory(string srcPath, string targetPath)
        {
            //先判断一下目标文件夹是否存在
            CheckIsDir(targetPath);
            //循环子文件夹
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
            foreach (FileSystemInfo file in fileinfo)
            {
                if (file is DirectoryInfo)   //判断是否文件夹
                {
                    CheckIsDir(targetPath + "\\" + file.Name);
                    CopyDirectory(file.FullName, targetPath + "\\" + file.Name);    //递归调用复制子文件夹
                }
                else
                {
                    File.Copy(file.FullName, targetPath + "\\" + file.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                }
            }
        }
        private static void CheckIsDir(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);   //目标目录下不存在此文件夹即创建子文件夹
            }
        }

    }
}
