using Aaron.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenBrowser
{
    public partial class OpenBrowser : Form
    {
        public OpenBrowser()
        {
            InitializeComponent();
            Rectangle s = Screen.GetWorkingArea(this);
            var sx = s.Width;
            var sy = s.Height;

            var hight = this.Height;
            var width = this.Width;
            //获取中间坐标  这个控件放在左边   然后高度还要再减去自己的一半才是在中间
            this.Location = new Point((sx / 2), (sy / 2) - (hight / 2));
            this.txtUrl.Select();
        }
        //<add key="ApiUrl" value="http://localhost:5008/HotUpdate/UpdateController"/>
        // <add key="AdminUrl" value="http://localhost:5000/Activity/Index"/>
        string baseUrl = @"http://localhost:5000/Drive/Index?ActivityId=1&IndexId=%index%&activityFormId=1&ActivityTypeId=1&IsEdit=1";
        private void button1_Click(object sender, EventArgs e)
        {
            //admin只是热加载活动控制器的Url  baseUrl是具体的活动设置的Url
            GetHttpResponse("http://47.107.102.50:6016/HotUpdate");
            GetHttpResponse("http://47.107.102.50:5013/HotUpdate/UpdateController");
            GetHttpResponse("http://47.107.102.50:5013/HotUpdate/UpdateDLL");

            BrowserHelper.OpenBrowserUrl("http://47.107.102.50:6016/");
        }

        /// <summary>
        /// 只是请求一下热更新 让系统重新装载一下dll
        /// </summary>
        /// <param name="url"></param>
        private void GetHttpResponse(string url)
        {
            HttpHelper.GetHttpResponse(url);
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var index = this.txtIndex.Text;
            OpenIndexUrl(index);

        }


        private void OpenIndexUrl(string index)
        {
            var newUrl = baseUrl.Replace("%index%", index);
            BrowserHelper.OpenBrowserUrl(newUrl);
        }


        private void button4_Click(object sender, EventArgs e)
        {
            string ApiUrl = ConfigHelper.GetAppConfig("ApiUrl");
            string AdminUrl = ConfigHelper.GetAppConfig("AdminUrl");
            //改为用HttpClient求请求
            var res = HttpHelper.GetHttpResponse(ApiUrl);
            var res2 = HttpHelper.GetHttpResponse(AdminUrl);
            OpenIndexUrl("2");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string ApiUrl = ConfigHelper.GetAppConfig("ApiUrl");
            string AdminUrl = ConfigHelper.GetAppConfig("AdminUrl");
            //改为用HttpClient求请求
            var res = HttpHelper.GetHttpResponse(ApiUrl);
            var res2 = HttpHelper.GetHttpResponse(AdminUrl);
            var url = this.txtUrl.Text;
            BrowserHelper.OpenBrowserUrl(url);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string ApiUrl = ConfigHelper.GetAppConfig("ApiUrl");
            string AdminUrl = ConfigHelper.GetAppConfig("AdminUrl");
            //改为用HttpClient求请求
            var res = HttpHelper.GetHttpResponse(ApiUrl);
            var res2 = HttpHelper.GetHttpResponse(AdminUrl);
            var url = this.txtUrl.Text;
            BrowserHelper.OpenBrowserUrl("http://localhost:5000/ActivityHistory/Index?activityId=40");
        }
    }
}
