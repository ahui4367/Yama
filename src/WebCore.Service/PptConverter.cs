using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Slides;
using Aspose.Slides.Pptx;

namespace WebCore.Service
{
    public class PptConverter
    {
        /// <summary>
        /// 检查要转换的文件参数
        /// </summary>
        private static void CheckFile(string filename)
        {

            if (String.IsNullOrEmpty(filename))
            {
                throw new NullReferenceException("参数不能为空。");
            }

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("指定的文件不存在。", filename);
            }

            var fileExt = new FileInfo(filename).Extension.ToLower();
            if (fileExt != ".ppt" && fileExt != ".pptx")
            {
                throw new Exception("文件类型必须为幻灯片 ppt, pptx 格式。");
            }
        }

        private static string GetImageExt(ImageFormat format)
        {

            if (format == ImageFormat.Jpeg)
            {
                return ".jpeg";
            }
            else if (format == ImageFormat.Png)
            {
                return ".png";
            }
            return "";
        }

        /// <summary>
        /// 
        /// 在当前目录转换幻灯片文件为图片文件
        /// 
        /// 输入：
        /// d:\file\abc.pptx 
        /// 
        /// 或：
        /// 
        /// d:\file\abc.ppt
        /// 
        /// 转换为：
        /// d:\file\abc-1.png 
        /// d:\file\abc-2.png 
        /// d:\file\abc-3.png
        /// 
        /// </summary>
        public static List<string> Convert(string filename)
        {

            return Convert(filename, ImageFormat.Png, 0);
        }

        public static List<string> Convert(string filename, ImageFormat format, int forceWidth)
        {

            CheckFile(filename);

            var fileExt = new FileInfo(filename).Extension.ToLower();

            return Convert(filename, fileExt, format, forceWidth);
        }

        /// <summary>
        /// 
        /// 在当前目录转换幻灯片文件为图片文件
        /// 根据指定的后缀来处理
        /// 
        /// </summary>
        public static List<string> Convert(string filename, string fileExt, ImageFormat format)
        {

            return Convert(filename, fileExt, format, 0);
        }

        public static List<string> Convert(string filename, string fileExt, ImageFormat format, int forceWidth)
        {

            if (fileExt == ".ppt")
            {
                return PPT(filename, format, forceWidth);
            }
            else if (fileExt == ".pptx")
            {
                return PPTX(filename, format, forceWidth);
            }

            return null;
        }

        public static List<string> PPTX(string filename, ImageFormat format, int forceWidth)
        {
            Size imageSize = Size.Empty;
            var fileInfo = new FileInfo(filename);
            var scale = 1.0f;
            var imageFileList = new List<string>();

            using (var pres = new PresentationEx(filename))
            {
                float w = pres.SlideSize.Size.Width;
                float h = pres.SlideSize.Size.Height;

                if (forceWidth > 0)
                {
                    imageSize = new Size(forceWidth, System.Convert.ToInt32(h * forceWidth / w));
                }
                uint i = 1;
                var slides = pres.Slides.Cast<SlideEx>().Where(p => p.SlideNumber > 0).OrderBy(p => p.SlideNumber);
                foreach (SlideEx slide in slides)
                {
                    DirectoryInfo dir = new DirectoryInfo(Path.Combine(AppConfig.DocRoot, Path.GetFileNameWithoutExtension(fileInfo.FullName)));
                    if (!dir.Exists)
                        dir.Create();

                    var imageFile = Path.Combine(dir.FullName, i.ToString() + GetImageExt(format));

                    if (forceWidth > 0)
                    {
                        slide.GetThumbnail(imageSize).Save(imageFile, format);
                    }
                    else
                    {
                        slide.GetThumbnail(scale, scale).Save(imageFile, format);
                    }
                    imageFileList.Add(imageFile);
                    i++;
                }
            }
            return imageFileList;
        }

        private static List<String> PPTX(string filename, ImageFormat format)
        {

            return PPTX(filename, format, 0);
        }

        public static List<string> PPT(string filename, ImageFormat format)
        {
            return PPT(filename, format, 0);
        }

        public static List<string> PPT(string filename, ImageFormat format, int forceWidth)
        {
            Size imageSize = Size.Empty;

            var fileInfo = new FileInfo(filename);

            var imageFileList = new List<string>();

            var pres = new Presentation(filename);

            int w = pres.SlideSize.Width;
            int h = pres.SlideSize.Height;

            if (forceWidth > 0)
            {
                imageSize = new Size(forceWidth, System.Convert.ToInt32(h * forceWidth / w));
            }

            uint i = 1;

            var slides = pres.Slides.Cast<Slide>().Where(p => !p.IsMasterSlide).OrderBy(p => p.SlidePosition);
            foreach (Slide slide in slides)
            {

                DirectoryInfo dir = new DirectoryInfo(Path.Combine(AppConfig.DocRoot, Path.GetFileNameWithoutExtension(fileInfo.FullName)));
                if (!dir.Exists)
                    dir.Create();

                var imageFile = Path.Combine(dir.FullName, i.ToString() + GetImageExt(format));

                if (forceWidth > 0)
                {
                    slide.GetThumbnail(imageSize).Save(imageFile, format);
                }
                else
                {
                    slide.GetThumbnail(1.0f, 1.0f).Save(imageFile, format);
                }

                imageFileList.Add(imageFile);
                i++;
            }

            return imageFileList;
        }
    }
}
