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
        public CopyViewForm()
        {
            InitializeComponent();
        }

        private void CopyViewForm_Load(object sender, EventArgs e)
        {
            //读取当前厂商
            var manuName = XMLHelper.GetNodeText(XMLPath.CopyViewManuName);
            this.txtManuName.Text = manuName;

            //初始厂商列表
            string listJson = XMLHelper.GetNodeText(XMLPath.CopyViewNameList);
            list = JsonHelper.DeserializeObject<List<string>>(listJson);
            //初始化两个坐标,后面生成的以这两个为基准
            int leftX = 0;
            int leftY = 0;
            int rigthX = 0;
            int rigthY = 0;
            //生成左边的 
            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    leftX = this.btnWsbgCopy.Location.X;
                    leftY = this.btnWsbgCopy.Location.Y - 60;

                    rigthX = this.btnMobileCopy.Location.X;
                    rigthY = this.btnMobileCopy.Location.Y - 60;
                }
                else
                {
                    leftY = leftY - 40;
                    rigthY = rigthY - 40;
                }
                Button btnLeft = new Button();
                btnLeft.Text = list[i];
                btnLeft.Location = new Point(leftX, leftY);
                btnLeft.Width = this.btnWsbgCopy.Width;
                btnLeft.Click += LeftBtnClick;
                this.Controls.Add(btnLeft);

                Button btnRight = new Button();
                btnRight.Text = list[i];
                btnRight.Location = new Point(rigthX, rigthY);
                btnRight.Width = this.btnWsbgCopy.Width;
                btnRight.Click += RightBtnClick;
                this.Controls.Add(btnRight);

            }
        }

        //路径太多,而且基本不会变,不用配置文件了直接写死
        private void WsbgCopy(object sender, EventArgs e)
        {
            //复制文件夹 先用米菲的youzuan来做好
            //出一个checkBox 用一个字段来绑定
            var isMifei = true;

            var baseViewPath = "";
            var targetSecondPath = "";
            if (isMifei)
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
            var srcPath = string.Format(@"{0}\Views\{1}", baseViewPath, manuName);
            var targetBasePath = @"E:\ZPCode\WsBg\WsBg.Web\Areas\";
            var targetPath = string.Format(@"{0}{1}\Views\{2}", targetBasePath, targetSecondPath, manuName);
            FileHelper.CopyDirectory(srcPath, targetPath);
            //复制js 重置复制路径
            srcPath = string.Format(@"{0}\PageJS\{1}", baseViewPath, manuName);
            targetPath = string.Format(@"{0}{1}\PageJS\{2}", targetBasePath, targetSecondPath, manuName);
            FileHelper.CopyDirectory(srcPath, targetPath);
            MessageBox.Show("复制完毕");
        }


        private void MobileCopy(object sender, EventArgs e)
        {
            var isMifei = false;
            var baseViewPath = "";
            if (isMifei)
            {
                //如果是米菲,根路径全部改为米菲
                baseViewPath = @"E:\ZPCode\Mifei_v2\MiFei.";
            }
            else
            {
                baseViewPath = @"E:\ZPCode\SuYa_V2\SuYa.";
            }

            var manuName = this.txtManuName.Text;
            var srcPath = string.Format(@"{0}{1}\Views\", baseViewPath, manuName);
            var targetBasePath = @"E:\ZPCode\SuYa_V2\SuYa.Mobile\Areas\";
            var targetPath = string.Format(@"{0}{1}\Views\", targetBasePath, manuName);
            FileHelper.CopyDirectory(srcPath, targetPath);
            //复制js 重置复制路径
            srcPath = string.Format(@"{0}{1}\PageJS\", baseViewPath, manuName);
            targetPath = string.Format(@"{0}{1}\PageJS\", targetBasePath, manuName);
            FileHelper.CopyDirectory(srcPath, targetPath);
        }

        /// <summary>
        /// 点击左边按钮触发
        /// </summary>
        private void LeftBtnClick(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var maunName = btn.Text.GetFirstInt().Trim();
            WsbgCopy(sender, e);
            XMLHelper.UpdateXMLList(list, maunName, XMLPath.CopyViewManuName);
        }

        /// <summary>
        /// 点击按钮触发
        /// </summary>
        private void RightBtnClick(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var maunName = btn.Text.GetFirstInt().Trim();
            MobileCopy(sender, e);
            XMLHelper.UpdateXMLList(list, maunName, XMLPath.CopyViewManuName);
        }


    }
}
