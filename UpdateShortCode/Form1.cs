﻿using Common;
using Models;
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


namespace UpdateShortCode
{
    public partial class Form1 : Form
    {
        public static List<string> list;
        public static List<Button> bList;

        private int max = 5;
        public Form1()
        {
            InitializeComponent();

            string listJson = XMLHelper.GetPath(XMLPath.OldMadnuId);
            list = JsonHelper.DeserializeObject<List<string>>(listJson);
            bList = new List<Button>() { btn1, btn2, btn3, btn4, btn5 };

            //初始化右边五个按键的字
            string sql = string.Format($@"SELECT  a.tb_manufacturerID,a.name,b.name AS domain FROM dbo.tb_manufacturer a LEFT JOIN tb_user b ON a.tb_manufacturerID=b.manufacturer_id WHERE tb_manufacturerID in({string.Join(",", list)}) AND b.system_role_id=-10");
            var models = SQLHelper.Query<List<tb_manu>>(sql);
            for (int i = 0; i < list.Count; i++)
            {
                bList[i].Text = list[i] + $"({models.First(c => c.tb_manufacturerID == list[i]).Name})";
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var newManuId = this.txt1.Text;
            UpdateAllShortCut(newManuId);

        }
        private void UpdateXML()
        {
            var jsonStr = JsonHelper.SerializeObject(list);
            XMLHelper.UpdateNodeInnerText(XMLPath.OldMadnuId, jsonStr);
        }

        private void BtnClick(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var maunId = btn.Text.GetFirstInt();
            UpdateAllShortCut(maunId);
        }

        private void UpdateAllShortCut(string newManuId)
        {

            var manuId = list.Last();
            string path = XMLHelper.GetPath(XMLPath.SQLShortCut);
            var files = Directory.GetFiles(path, "*.sqlpromptsnippet");
            foreach (var file in files)
            {
                string text = File.ReadAllText(file);
                string newText = text.Replace(manuId, newManuId);
                File.WriteAllText(file, newText, Encoding.UTF8);
            }
            if (list.Count() == max)
            {
                list.RemoveAt(0);
                list.Add(newManuId);
            }
            else
            {
                list.Add(newManuId);
            }
        }

    }
}