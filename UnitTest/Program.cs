using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string> { "12", "33", "55" };
            string str = list.GetSQLInWhere();
            Console.ReadLine();
        }


    }
}
