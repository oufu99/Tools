﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class IOHelper
    {
        public static void AddHost(string ip, string domain)
        {
            string path = XmlHelper.ReadText(XMLPath.Host);
            //通常情况下这个文件是只读的，所以写入之前要取消只读
            File.SetAttributes(path, File.GetAttributes(path) & (~FileAttributes.ReadOnly));//取消只读
            //1.创建文件流
            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    //127.0.0.1       shulunduo.web.cn
                    StringBuilder sb = new StringBuilder();
                    sb.Append("\r\n\t" + ip);
                    sb.Append("\t" + domain + ".web.cn\r\n");
                    sb.Append("\t" + ip);
                    sb.Append("\t" + domain + ".mobile.cn\r\n");
                    sw.WriteLine(sb.ToString());
                }
            }
        }
    }
}
