using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Reflect
{
    class Program
    {
        static void Main(string[] args)
        {
            SFCompany p = new SFCompany();
            p.CompayName = "SF";
            p.Moeny = 19;
            string msg = QueryHelper.Query(p);

            Console.WriteLine(msg);
            Console.ReadLine();

        }
    }
}
