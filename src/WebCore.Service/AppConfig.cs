using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebCore.Service
{
    public sealed class AppConfig
    {
        static AppConfig()
        {
            string value = ConfigurationManager.AppSettings["Upload.Root"];
            if (!string.IsNullOrEmpty(value) && HttpContext.Current != null)
            {

                UploadRoot = HttpContext.Current.Server.MapPath(value);
            }

            value = ConfigurationManager.AppSettings["Doc.Root"];
            if (!string.IsNullOrEmpty(value) && HttpContext.Current != null)
            {

                DocRoot = HttpContext.Current.Server.MapPath(value);
            }

            value = ConfigurationManager.AppSettings["Video.Root"];
            if (!string.IsNullOrEmpty(value) && HttpContext.Current != null)
            {

                VideoRoot = HttpContext.Current.Server.MapPath(value);
            }

            value = ConfigurationManager.AppSettings["Video.Tool"];
            if (!string.IsNullOrEmpty(value) && HttpContext.Current != null)
            {
                VideoTool = HttpContext.Current.Server.MapPath(value);
            }
        }

        public static string UploadRoot { get; set; }

        public static string DocRoot { get; set; }

        public static string VideoRoot { get; set; }

        public static string VideoTool { get; set; }
    }
}