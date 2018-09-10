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
using Common;
using Models;

namespace AddNewManu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.txtId.TabIndex = 0;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            //选择文件,然后复制
            string filePath = "";
            string fileName = "";


            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件|*.*"; //设置要选择的文件的类/设置要选择的型
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = fileDialog.FileName;//返回文件的完整路径     
                fileName = fileDialog.SafeFileName;
            }
            string manuId = this.txtId.Text.Trim();
            string name = this.txtName.Text.Trim();

            string sql = string.Format($@"SELECT a.name,b.name AS domain FROM dbo.tb_manufacturer a LEFT JOIN tb_user b ON a.tb_manufacturerID=b.manufacturer_id WHERE tb_manufacturerID={manuId} AND b.system_role_id=-10");
            var model = SQLHelper.Query<tb_manu>(sql);
            if (name == "请输入厂商名" || string.IsNullOrEmpty(name))
            {
                name = model.Name;
            }

            string projectPath = XMLHelper.GetPath(XMLPath.CompanyProject);
            string path = projectPath + name;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                string file1 = $@"{path}\{name}.sql";
                File.Create(file1);
                string file2 = $@"{path}\{name}url.txt";
                File.Create(file2);
                string file3 = $@"{path}\{fileName}";
                File.Move(filePath, file3);
            }

            //添加IIS的绑定
            string domain = model.Domain;
            IISHelp.AddHostHeader(domain);
            //写入host
            IOHelper.AddHost(XMLHelper.GetPath(XMLPath.HostIp), domain);
            MessageBox.Show("创建成功");
            this.Close();
        }

        private void txt1_TextChanged(object sender, EventArgs e)
        {

        }
    }

   
}
