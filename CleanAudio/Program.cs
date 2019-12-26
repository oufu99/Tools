using Aaron.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanAudio
{
    /// <summary>
    /// 专门用来洗网易云上显示错误的那种音频
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
  
            //新增外层是父目录
            var basePath = @"D:\MyLove";
            var paths = Directory.GetDirectories(basePath).ToList();
            //包含父目录
            paths.Add(basePath);
            foreach (var item in paths)
            {
                var dirPath = item;
                if (dirPath[dirPath.Length - 1] != '\\')
                {
                    dirPath += @"\";
                }
                string targetBasePath = dirPath + @"\temp\";
                FileHelper.CreateDirectory(targetBasePath);
                var files = Directory.GetFiles(dirPath);
                foreach (var file in files)
                {
                    var path = file.Trim();
                    TagLib.File f = TagLib.File.Create(path);

                    var name = Path.GetFileName(path);
                    //网易读取的名字
                    f.Tag.Title = name;
                    //说明的左边
                    f.Tag.Performers = new string[] { name };
                    //说明的右边
                    f.Tag.Album = name;

                    f.Tag.Pictures = null;
                    f.Save();
                    var targetPath = targetBasePath + name;
                    FileHelper.MoveFile(file, targetPath);
                }
            }



            Console.WriteLine("清洗完毕");
            Console.ReadLine();




        }
    }
}
