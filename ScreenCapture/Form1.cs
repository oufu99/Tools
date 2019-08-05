using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using ZXing;
using Aaron.Common;

namespace ScreenCapture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static int x = 0;
        static int y = 0;
        static int nowX = 0;
        static int nowY = 0;
        static bool isMouseClick = false;
        static Graphics g;
        static int width = 0;
        static int height = 0;
        // static Graphics gi; 
        static Bitmap bmp;
        static string filename = "1.jpg";
        static string saveFile = XMLHelper.GetNodeText(XMLPath.ErWeiMaPath);
        static bool isOneDown = true;
        static Bitmap bm;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Q)//判断回车键
            {
                return true;
            }
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //能用以后再放到管理工具里用全局快捷键来调用
            Size size = Screen.PrimaryScreen.Bounds.Size;
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(0, 0, 0, 0, size);

            //注意以下顺序。 
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = false;
            //把当前屏幕保存到临时文件下
            bmp.Save(filename, ImageFormat.Jpeg);
            g = this.CreateGraphics();
            this.Opacity = 0.5;
        }

        /// <summary>
        /// 鼠标按下事件  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            if (isOneDown)
            {
                x = MousePosition.X;
                y = MousePosition.Y;
                isMouseClick = true;
                isOneDown = false;
            }
        }

        /// <summary>
        /// 放下鼠标触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {

            if (isMouseClick)
            {
                // MessageBox.Show("放开后鼠标的位置："+MousePosition.X.ToString() + "" + MousePosition.Y.ToString()); 
                nowX = MousePosition.X + 1;
                nowY = MousePosition.Y + 1;

                Image newImage = Image.FromFile(filename);
                Rectangle destRect = new Rectangle(x, y, nowX - x, nowY - y);
                bmp = new Bitmap(nowX - x, nowY - y);
                bm = ((Bitmap)newImage).Clone(destRect, newImage.PixelFormat);
                bm.Save(saveFile);
                newImage.Dispose();
                isMouseClick = false;
            }
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseClick)
            {
                width = Math.Abs(MousePosition.X - x);
                height = Math.Abs(MousePosition.Y - y);
                g = CreateGraphics();
                g.Clear(BackColor);
                g.FillRectangle(Brushes.Navy, x < MousePosition.X ? x : MousePosition.X, y
 < MousePosition.Y ? y : MousePosition.Y, width + 1, height + 1);
            }
        }

        /// <summary>
        /// 双击窗口 关闭页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            bm.Dispose();
            File.Delete(filename);
            Bitmap m2 = new Bitmap(saveFile);
            string url = DecodeQrCode(m2);
            if (!string.IsNullOrEmpty(url))
            {
                Clipboard.SetText(url);
            }
            else
            {
                Clipboard.SetText("未解析出地址"); 
            }
        }

        /// <summary>
        /// 解码二维码
        /// </summary>
        /// <param name="barcodeBitmap">待解码的二维码图片</param>
        /// <returns>扫码结果</returns>
        private string DecodeQrCode(Bitmap barcodeBitmap)
        {
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";
            var result = reader.Decode(barcodeBitmap);
            return (result == null) ? null : result.Text;
        }
    }
}