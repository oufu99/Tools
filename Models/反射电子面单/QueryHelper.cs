using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class QueryHelper
    {
        public static string Query(ICompany cp)
        {
            var assemblyName = ConfigurationManager.AppSettings["AssemblyName"];
            var completeName = "Models." + assemblyName;
            Type type = Type.GetType(completeName, false, false);
            var method = type.GetMethod("Query");
            var obj = Activator.CreateInstance(type);
            var msg = method.Invoke(obj, new object[] { cp });

            return msg.ToString();

        }
    }
}
