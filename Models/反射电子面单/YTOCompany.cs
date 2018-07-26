using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class YTOCompany : ICompany
    {
        static YTOCompany()
        {

        }
        public string Name { get; set; }
        public int Price { get; set; }
        public string CompayName { get; set; }
    }
}
