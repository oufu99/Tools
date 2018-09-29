using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Windows.Forms;

namespace UpdateShortCode
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    string manuId = XMLHelper.GetPath(XMLPath.OldMadnuId);
        //    Console.WriteLine($"上一次修改的厂商{manuId}");
        //    Console.WriteLine("请输入新厂商Id,如果要改变上一次的请输入 old ");
        //    string newManuId = Console.ReadLine();
        //    if (newManuId == "old")
        //    {
        //        Console.WriteLine("请输入旧厂商Id");
        //        manuId = Console.ReadLine();
        //    }
        //    string path = XMLHelper.GetPath(XMLPath.SQLShortCut);
        //    var files = Directory.GetFiles(path, "*.sqlpromptsnippet");
        //    foreach (var file in files)
        //    {
        //        string text = File.ReadAllText(file);
        //        string newText = text.Replace(manuId, newManuId);
        //        File.WriteAllText(file, newText, Encoding.UTF8);
        //    }
        //    XMLHelper.UpdateNodeInnerText(XMLPath.OldMadnuId, newManuId);
        //    Console.WriteLine("修改完毕");
        //}



        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

    }
}
