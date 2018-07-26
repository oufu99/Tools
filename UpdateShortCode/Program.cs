﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateShortCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入旧厂商Id");
            string manuId = Console.ReadLine();
            Console.WriteLine("请输入新厂商Id");
            string newManuId = Console.ReadLine();
            string path = @"C:\Users\Administrator\AppData\Local\Red Gate\SQL Prompt 7\Snippets";
            var files = Directory.GetFiles(path, "*.sqlpromptsnippet");
            foreach (var file in files)
            {
                string text = File.ReadAllText(file);
                string newText = text.Replace(manuId, newManuId);
                File.WriteAllText(file, newText, Encoding.UTF8);
            }
            Console.WriteLine("修改完毕");
            Console.ReadLine();
        }
    }
}
