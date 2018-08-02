using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace LoyalFilial.APIService.Com
{
    /// <summary>
    /// HandlerVerification 的摘要说明
    /// </summary>
    public class HandlerVerification : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            AuthCode(Guid.NewGuid().ToString(), context);
        }

        #region 图片验证码
        /// <summary>
        /// 二维码生成请求入口
        /// </summary>
        /// <param name="code"></param>
        public void AuthCode(string code, HttpContext context)
        {

            if (context.Session["ImgAuthCode_RequestCode"] != null && context.Session["ImgAuthCode_RequestCode"].ToString() != code)
                return;
            var imgCode = GenCode(4);
            if (string.IsNullOrEmpty(imgCode))
                return;
            using (var img = GenImg(imgCode))
            {
                context.Session["ImgAuthCode_Key"] = imgCode;
                //清除缓冲区流中的所有输出
                context.Response.ClearContent();
                //输出流的HTTP MIME类型设置为"image/Png"
                context.Response.ContentType = "image/Png";
                //输出图片的二进制流
                context.Response.BinaryWrite(img.ToArray());
            }
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="num">字母数量</param>
        /// <returns>返回字符</returns>
        private string GenCode(int num)
        {
            string[] source =
                {
                    "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H",
                    "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "W", "X", "Y"
                };
            string code = "";
            //创建Random类的实例
            var rd = new Random();
            //获取验证码
            for (int i = 0; i < num; i++)
            {
                code += source[rd.Next(0, source.Length)];

            }
            //返回产生的验证码
            return code;
        }

        /// <summary>
        /// 生成图片
        /// </summary>
        /// <param name="checkCode"></param>
        private MemoryStream GenImg(string checkCode)
        {
            //根据验证码的长度确定输出图片的宽度
            int iWidth = (int)Math.Ceiling(checkCode.Length * 30m) - 30;
            const int iHeight = 30;
            //int iWidth = 91, iHeight = 41;

            //创建图像
            var image = new Bitmap(iWidth, iHeight);
            //从图像获取一个绘图面
            Graphics g = Graphics.FromImage(image);

            try
            {
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的前景噪音点50个
                for (int j = 5; j < 30; j += 3)
                {
                    for (int i = 5; i < 90; i++)
                    {
                        if (i % 2 == 1) continue;
                        int x = i;
                        int y = j;
                        image.SetPixel(x, y, Color.Silver);
                    }
                }
                //画图片的前景噪音点50个
                for (int j = 5; j < 90; j += 5)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        if (i % 2 == 1) continue;
                        int x = j;
                        int y = i;
                        image.SetPixel(x, y, Color.Silver);
                    }
                }
                //画图片的框线
                g.DrawRectangle(new Pen(Color.White), 0, 0, image.Width - 1, image.Height - 1);

                //定义随机数
                Random rand = new Random();
                //定义字体          
                string[] fontArray = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体", "黑体", "楷体", "隶书", "华文新魏", "华文黑细" };
                //定义颜色         
                Color[] c = { Color.Tomato, Color.DarkBlue, Color.Green, Color.DarkRed, Color.Brown, Color.DarkCyan, Color.Purple, Color.Yellow, Color.Blue, Color.Wheat };
                //定义颜色         
                FontStyle[] FS = { FontStyle.Bold, FontStyle.Italic, FontStyle.Strikeout, FontStyle.Underline };
                //定义绘制文字的字体

                //线性渐变画刷
                var brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), c[rand.Next(5)],
                                                    c[rand.Next(9)], 1.2f, true);

                for (int i = 0; i < checkCode.Length; i++)
                {
                    int size = rand.Next(20);
                    if (i < 3) g.DrawString(checkCode.Substring(i, 1), new Font(fontArray[rand.Next(8)], (size < 16 ? 18 : size), (FS[rand.Next(3)])), brush, (i * 24)+2, rand.Next(4));
                    else g.DrawString(checkCode.Substring(i, 1), new Font(fontArray[rand.Next(8)], (size < 16 ? 18 : size), (FS[rand.Next(3)])), brush, 61, 1);
                }


                //创建内存流用于输出图片
                using (var ms = new MemoryStream())
                {
                    //图片格式制定为png
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms;
                }
            }
            finally
            {
                //释放Bitmap对象和Graphics对象
                g.Dispose();
                image.Dispose();
            }
        }

        #endregion


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}