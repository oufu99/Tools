using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SFQuery : IQuery
    {
        public string Query(ICompany p)
        {
            SFCompany model = p as SFCompany;
            //dosomething....
            return "顺丰的钱是" + model.Moeny;
        }
        public string Query2(object p)
        {
            SFCompany model = p as SFCompany;
            //dosomething....
            return "顺丰的钱是" + model.Moeny;
        }

    }
}
