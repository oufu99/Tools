using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NavicatModel
    {
        public string name { get; set; }
        public string connType { get; set; }
        public string label { get; set; }
        public string comment { get; set; }
        public string text { get; set; }
        public List<string> placeholders { get; set; }
    }
}
