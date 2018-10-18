using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinformRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            //string strURL = "http://meixin.winmobi.cn/api/gettoken.aspx";

            string strURL = "http://192.168.0.104";
            HttpWebRequest request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            //Post请求方式
            request.Method = "POST";
            // 内容类型
            request.ContentType = "application/x-www-form-urlencoded";
            // 参数经过URL编码
            string paraUrlCoded = "CustID=102268";
            byte[] payload;
            //将URL编码后的字符串转化为字节
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            //设置请求的 ContentLength 
            request.ContentLength = payload.Length;
            //获得请求流
            using (System.IO.Stream writer = request.GetRequestStream())
            {
                //将请求参数写入流
                writer.Write(payload, 0, payload.Length);

                HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string responseText = reader.ReadToEnd();
                    Console.WriteLine(responseText);
                }
                Console.ReadLine();
            }


        }
    }
}
