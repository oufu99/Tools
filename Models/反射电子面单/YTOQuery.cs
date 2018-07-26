using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class YTOQuery : IQuery
    {
        public string Query(ICompany p)
        {
            YTOCompany model = p as YTOCompany;
            //dosomething....
            return "圆通的钱是" + model.Price;
        }
    }
}
