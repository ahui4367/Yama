/***************************************************************************************
 *
 * 功能说明：目录帮助类
 *
 * 当前文件：DirHelper.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Utils.IO
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Web;
    using System.Web.Hosting;

    public sealed class DirHelper
    {
        #region Properties

        /// <summary>
        /// 是否在webform环境下
        /// </summary>
        public static bool IsAspNet
        {
            get
            {
                return !string.IsNullOrEmpty(HttpRuntime.AppDomainAppId);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 检查目录是否酆存在
        /// </summary>
        /// <param name="directory">目录名</param>
        /// <param name="create">如果为真，则在不存在的情况下，自动创建</param>
        /// <returns></returns>
        public static bool DirExists(string directory, bool create = false)
        {
            if (!string.IsNullOrEmpty(directory))
            {
                directory = GetPhysicalPath(directory);
                if (create)
                {
                    Directory.CreateDirectory(directory);
                    return true;
                }
                else
                {
                    return Directory.Exists(directory);
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取应用程序的根目录
        /// </summary>
        /// <returns></returns>
        public static string GetAppRoot()
        {
            if (!IsAspNet)
            {
                return Thread.GetDomain().BaseDirectory;
            }
            else
            {
                return HttpRuntime.AppDomainAppVirtualPath;
            }
        }

        /// <summary>
        /// 获得目录下指定模板的文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="extName"></param>
        /// <returns></returns>
        public static List<string> GetFiles(string path, string extName)
        {
            List<string> result = new List<string>();
            if (Directory.Exists(path))
            {
                result.AddRange(Directory.GetFiles(path, extName));
            }

            return result;
        }

        /// <summary>
        /// 根据虚拟路径，获取物理目录
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public static string GetPhysicalPath(string virtualPath)
        {
            if (VirtualPathUtility.IsAbsolute(virtualPath))
            {
                if (IsAspNet)
                {
                    virtualPath = VirtualPathUtility.ToAppRelative(virtualPath);
                }
                else
                {
                    virtualPath = Path.Combine(GetAppRoot(), virtualPath);
                }
            }

            if (VirtualPathUtility.IsAppRelative(virtualPath))
            {
                if (IsAspNet)
                {
                    virtualPath = HostingEnvironment.MapPath(virtualPath);
                }
                else
                {
                    virtualPath = Path.Combine(GetAppRoot(), virtualPath.Substring(2));
                }
            }

            return virtualPath;
        }

        /// <summary>
        /// 获取Web应用的虚拟根目录
        /// </summary>
        /// <returns></returns>
        public static string GetWebRoot()
        {
            if (!IsAspNet)
            {
                return "/";
            }
            else
            {
                return HttpRuntime.AppDomainAppVirtualPath;
            }
        }

        #endregion Methods
    }
}