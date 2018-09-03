using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
           var noteList= XmlHelper.ReadNodes(@"configuration/CSharpShortCut");

            Console.ReadLine(); 
        }
    }
}
