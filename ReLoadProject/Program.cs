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
            //传入参数  只要是打开这个项目,肯定是有参数传入,没有就报错然后改
            var projectName = args[0];
            //要重启的项目 应该是都要重新编译一次的,不然没必要重启
            var projectPath = $@"D:\Tools\{projectName}\{projectName}.csproj";
            AutoBuildHelper.BuildOutBin(projectPath);
            var path = $@"D:\Tools\{projectName}\bin\Debug\{projectName}.exe";
            Process.Start(path);
            Console.ReadLine();

        }
    }
}
