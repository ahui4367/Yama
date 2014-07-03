﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesHJ.Fx.Pattern;

namespace WebCore.Service
{
    public class CaptchaBuilder : Singleton<CaptchaBuilder>
    {
        private static int RandomSeed
        {
            get
            {
                return Guid.NewGuid().GetHashCode();
            }
        }

        private Random _random;

        private CaptchaBuilder()
        {
            _random = new Random(RandomSeed);
        }

        public string MakeCode(int len = 4)
        {
            int val = 0;
            string result = string.Empty;
            if (len > 0)
            {
                for (int i = 0; i < len; i++)
                {
                    val = _random.Next();
                    if (val % 2 == 0)
                    {
                        result += (char)('0' + val % 10);
                    }
                    else
                    {
                        result += (char)('A' + val % 26);
                    }
                }
            }

            return result;
        }

        public MemoryStream MakeCaptcha(ref string code, int len = 4)
        {
            code = string.IsNullOrEmpty(code) ? MakeCode(len) : code.Trim();

            Bitmap image = new Bitmap((int)Math.Ceiling((code.Length * 12.5)), 22);
            Graphics graphic = Graphics.FromImage(image);

            try
            {
                Random random = new Random();

                graphic.Clear(Color.White);

                int x1 = 0, y1 = 0, x2 = 0, y2 = 0;

                for (int index = 0; index < 25; index++)
                {
                    x1 = random.Next(image.Width);
                    x2 = random.Next(image.Width);
                    y1 = random.Next(image.Height);
                    y2 = random.Next(image.Height);

                    graphic.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Red, Color.DarkRed, 1.2f, true);
                graphic.DrawString(code, font, brush, 2, 2);

                int x = 0;
                int y = 0;

                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    x = random.Next(image.Width);
                    y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                graphic.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                //将图片验证码保存为流Stream返回
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms;
            }
            finally
            {
                graphic.Dispose();
                image.Dispose();
            }
        }
    }
}
