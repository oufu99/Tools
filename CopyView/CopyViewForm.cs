using Common;
using Common.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyView
{
    public partial class CopyViewForm : Form
    {
        List<string> list = new List<string>();
        List<KeyValueModel> listManu = new List<KeyValueModel>();

        bool IsMifei = true;
        public CopyViewForm()
        {
            InitializeComponent();

        }
        private void CopyViewForm_Load(object sender, EventArgs e)
        {
            var checkBoxHistory = XMLHelper.GetNodeText(XMLPath.CopyViewExeCheckBox);
            IsMifei = checkBoxHistory == "1";
            this.cboxIsMifei.Checked = IsMifei;

            //加载初始厂商列表
            string listJson = XMLHelper.GetNodeText(XMLPath.CopyViewNameList);
            list = JsonHelper.DeserializeObject<List<string>>(listJson);
            this.ddlManu.DataSource = list;
            this.txtManuName.Text = list.Count == 0 ? "" : list[0];
        }

        //路径太多,而且基本不会变,不用配置文件了直接写死
        private void WsbgCopy(object sender, EventArgs e)
        {
            //复制文件夹 先用米菲的youzuan来做好
            //出一个checkBox 用一个字段来绑定
            var baseViewPath = "";
            var targetSecondPath = "";
            if (IsMifei)
            {
                //如果是米菲,根路径全部改为米菲
                baseViewPath = @"E:\ZPCode\WsBg\MiFei.Custom";
                targetSecondPath = "mifeicustom";
            }
            else
            {
                baseViewPath = @"E:\ZPCode\WsBg\Suya.Custom";
                targetSecondPath = "suyacustom";
            }

            var manuName = this.txtManuName.Text;
            //要复制文件夹的 名字
            var srcPath = string.Format(@"{0}\Views\{1}", baseViewPath, manuName);
            var targetBasePath = @"E:\ZPCode\WsBg\WsBg.Web\Areas\";
            var targetPath = string.Format(@"{0}{1}\Views\{2}", targetBasePath, targetSecondPath, manuName);
            FileHelper.CopyDirectory(srcPath, targetPath);
            //复制js 重置复制路径
            srcPath = string.Format(@"{0}\PageJS\{1}", baseViewPath, manuName);
            targetPath = string.Format(@"{0}{1}\PageJS\{2}", targetBasePath, targetSecondPath, manuName);
            FileHelper.CopyDirectory(srcPath, targetPath);
            MessageBox.Show("复制完毕");
            UpdateHistory();
        }


        private void MobileCopy(object sender, EventArgs e)
        {
            var baseViewPath = "";
            var targetBasePath = "";
            if (IsMifei)
            {
                //如果是米菲,根路径全部改为米菲
                baseViewPath = @"E:\ZPCode\Mifei_v2\MiFei.";
                targetBasePath = @"E:\ZPCode\Mifei_v2\Mifei.Mobile\Areas\";
            }
            else
            {
                baseViewPath = @"E:\ZPCode\SuYa_V2\SuYa.";
                targetBasePath = @"E:\ZPCode\SuYa_V2\SuYa.Mobile\Areas\";
            }

            var manuName = this.txtManuName.Text;
            var srcPath = string.Format(@"{0}{1}\Views\", baseViewPath, manuName);

            var targetPath = string.Format(@"{0}{1}\Views\", targetBasePath, manuName);
            FileHelper.CopyDirectory(srcPath, targetPath);
            //复制js 重置复制路径
            srcPath = string.Format(@"{0}{1}\PageJS\", baseViewPath, manuName);
            targetPath = string.Format(@"{0}{1}\PageJS\", targetBasePath, manuName);
            FileHelper.CopyDirectory(srcPath, targetPath);
            MessageBox.Show("复制完毕");
            UpdateHistory();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var manuName = this.ddlManu.SelectedValue.ToString();
            this.txtManuName.Text = manuName;
        }


        public void UpdateHistory()
        {
            XMLHelper.UpdateXMLList(list, XMLPath.CopyViewNameList, this.txtManuName.Text);
            IsMifei = this.cboxIsMifei.Checked;
            XMLHelper.UpdateNodeInnerText(XMLPath.CopyViewExeCheckBox, IsMifei ? "1" : "0");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XMLHelper.UpdateXMLList(list, XMLPath.CopyViewNameList, "mifei");
        }
    }
}
