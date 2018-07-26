using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SFCompany : ICompany
    {
        static SFCompany()
        {

        }
        public string Name { get; set; }
        public int Moeny { get; set; }
        public string CompayName { get; set; }
    }
}
