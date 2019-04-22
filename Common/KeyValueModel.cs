using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class KeyValueModel
    {
        //此类常用.而且不会变动  有些项目只会引用Common,所以把这类放在Common里面
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
