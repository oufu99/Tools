using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ListExtends
    {
        public static string GetSQLInWhere(this List<string> list)
        {
            string where = "";
            list.ForEach(c => where += "\"" + c + "\"" + ",");
            if (!string.IsNullOrEmpty(where))
            {
                where = where.Remove(where.Length - 1);
            }
            return where;
        }
    }
}
