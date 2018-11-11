using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal class InterTest
    {
        public string Name { get; set; }
        public int Age { get; set; }

        internal void SayHi()
        {
            Console.WriteLine(Name + "====" + Age);
        }
    }
}
