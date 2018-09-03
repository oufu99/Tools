using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class HostHelp
    {
        public static void EditHost(string Name, string ManuID, string CompanyName, string ip, string hostName)
        {
            string path = @"C:\Windows\System32\drivers\etc\hosts";
            //通常情况下这个文件是只读的，所以写入之前要取消只读
            File.SetAttributes(path, File.GetAttributes(path) & (~FileAttributes.ReadOnly));//取消只读
            //1.创建文件流
            FileStream fs = new FileStream(path, FileMode.Append);
            //2.创建写入器
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            //3.开始写入
            bool result = false;//标识是否写入成功
            try
            {
                //#本生源	10606 	河南本生源生物科技有限公司
                //127.0.0.1       benshengyuan.serpmobile3.com

                StringBuilder sb = new StringBuilder();
                sb.Append("\r\n\t#" + Name + "	" + ManuID + " 	" + CompanyName + "\r\n");
                sb.Append("\t" + ip);
                sb.Append("\t" + hostName + ".serpmobile3.com\r\n");
                sb.Append("\t" + ip);
                sb.Append("\t" + hostName + ".serpmanage3.com\r\n");
                sw.WriteLine(sb.ToString());

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                //4.关闭写入器
                if (sw != null)
                {
                    sw.Close();
                }
                //5.关闭文件流
                if (fs != null)
                {
                    fs.Close();
                }
            }
            if (result == true)
            {
                // File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.ReadOnly);//设置只读
            }
        }


    }
}
