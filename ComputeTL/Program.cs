using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputeTL
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal sum = 0;
            for (decimal i = 1; i <= 25; i++)
            {
                sum += Math.Round((decimal)1 / 25, 5) * Math.Round((decimal)i / 10, 5);
            }

            Console.ReadLine();
        }
    }
}
