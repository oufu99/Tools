using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddIIS
{
    class Program
    {
        static void Main(string[] args)
        {
            string domain = "test";
            AddHostHeader(1, "", 80, domain + ".web.cn");
            AddHostHeader(2, "", 80, domain + ".mobile.cn");
        }

        public static void AddHostHeader(int siteid, string ip, int port, string domain)//增加主机头（站点编号.ip.端口.域名）
        {
            DirectoryEntry site = new DirectoryEntry("IIS://localhost/W3SVC/" + siteid);
            PropertyValueCollection serverBindings = site.Properties["ServerBindings"];
            string headerStr = string.Format("{0}:{1}:{2}", ip, port, domain);

            if (!serverBindings.Contains(headerStr))
            {
                serverBindings.Add(headerStr);
            }
            site.CommitChanges();
        }

    }
}
