using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Models;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory facotry = new Factory();
            facotry.SetName("Aaron");
            facotry.SayHi();
            Console.ReadLine();
        }


    }
}
