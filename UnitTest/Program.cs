using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //打开chrome浏览器
            var path = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            string myArgs = "meixin.onlyid.cn/SysAdmin/Tools/Login.ashx?domain=meixin&manuId=10646";
            ProcessStartInfo startInfo = new ProcessStartInfo(path);
            startInfo.Arguments = myArgs;
            Process.Start(startInfo);

            Console.ReadLine();
        }


    }
}
