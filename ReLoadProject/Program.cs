using Aaron.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReLoadProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "";
            //传入参数  只要是打开这个项目(都是用Aaron.Common方法打开的),肯定是有参数传入,没有就报错然后改
            var projectName = args[0];
            if (projectName == "HaveFullPath")
            {
                path = args[1];
                Console.WriteLine(path);
            }
            else
            {
                //要重启的项目 应该是都要重新编译一次的,不然没必要重启
                var projectPath = $@"D:\Tools\{projectName}\{projectName}.csproj";
                AutoBuildHelper.BuildOutBin(projectPath);
                Console.WriteLine("主项目编译完毕!");
                if (projectName == "OpenMyTools")
                {
                    string classProjectPath = @"D:\Tools\Common\Common.csproj";
                    AutoBuildHelper.BuildOutBin(classProjectPath);
                    Console.WriteLine("Common编译完毕!");
                    //复制Common到OpenTools目录去
                    FileHelper.CopyFile(@"D:\Tools\Common\bin\Debug\Common.dll", @"D:\Tools\OpenMyTools\bin\Debug\Common.dll");
                    Console.WriteLine("Common复制完毕!");
                }
                path = $@"D:\Tools\{projectName}\bin\Debug\{projectName}.exe";
            }
            Process.Start(path);
        }
    }
}
