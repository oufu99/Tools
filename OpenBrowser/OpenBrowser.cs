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
        }

        string baseUrl = @"http://localhost:5000/Drive/Index?ActivityId=1&IndexId=%index%&activityFormId=1&ActivityTypeId=1";
        private void button1_Click(object sender, EventArgs e)
        {
            //admin只是热加载活动控制器的Url  baseUrl是具体的活动设置的Url
            string ApiUrl = ConfigHelper.GetAppConfig("ApiUrl");
            string AdminUrl = ConfigHelper.GetAppConfig("AdminUrl");
            BrowserHelper.OpenBrowserUrl(ApiUrl);
            BrowserHelper.OpenBrowserUrl(AdminUrl);
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
    }
}
