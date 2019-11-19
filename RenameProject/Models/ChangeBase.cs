using Aaron.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameProject.Models
{
    public abstract class ChangeBase
    {
        public string DirPath { get; set; }
        public string TargetPath { get; set; }
        public ChangeBase(string _dirPath)
        {
            this.DirPath = _dirPath;
            TargetPath = _dirPath + @"\temp";
            FileHelper.CreateDirectory(TargetPath);
        }

        public void Rename()
        {
            //调用Change方法得到一个新的名字,然后在这里移动到指定目录
            var list = new List<string>();
            list = FileHelper.GetDirectorAllFile(DirPath, list);
            foreach (var item in list)
            {
                var newFullPath = GetNewFileName(item);
                FileHelper.MoveFile(item, newFullPath);

            }
        }


       public abstract string GetNewFileName(string name);

        //实际处理文件名逻辑
        public abstract string ConvertName(string name);
        

    }
}
