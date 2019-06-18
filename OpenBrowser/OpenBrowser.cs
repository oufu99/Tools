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

        private void button1_Click(object sender, EventArgs e)
        {
            string ApiUrl = ConfigHelper.GetAppConfig("ApiUrl");
            string AdminUrl = ConfigHelper.GetAppConfig("AdminUrl");
            BrowserHelper.OpenBrowserUrl(ApiUrl);
            BrowserHelper.OpenBrowserUrl(AdminUrl);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BrowserHelper.OpenBrowserUrl(@"http://localhost:63476/Drive/Index?ActivityId=1&IndexId=2&activityFormId=1&ActivityTypeId=1");
        }
    }
}
