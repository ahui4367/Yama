using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebSite.Admin.Controllers
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

        }

        public static string UploadRoot { get; set; }

    }
}