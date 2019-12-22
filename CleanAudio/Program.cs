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

            //string path = @"E:\Test\59《卷上·徐爱录》心即理，天下无心外之事，无心外之理.mp3";
            //TagLib.File f = TagLib.File.Create(path);
            //f.Tag.Album = "认知方法论";
            //f.Save();

            string dirPath = @"E:\认知方法论\音频归类\";

            string targetBasePath = dirPath + @"\temp\";
            FileHelper.CreateDirectory(targetBasePath);
            var files = Directory.GetFiles(dirPath);
            foreach (var item in files)
            {
                var path = item.Trim();
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
                FileHelper.MoveFile(item, targetPath);
            }

            Console.WriteLine("清洗完毕");
            Console.ReadLine();




        }
    }
}
