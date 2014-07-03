using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebCore.Service
{
    public class VideoConverter
    {
        public static void Convert(string file)
        {
            var fileInfo = new FileInfo(file);
            if (fileInfo.Exists)
            {
                DirectoryInfo dir = new DirectoryInfo(AppConfig.VideoRoot);
                if (!dir.Exists)
                {
                    dir.Create();
                }

                var outFile = Path.Combine(dir.FullName, Path.GetFileNameWithoutExtension(file) + ".flv");

                ProcessStartInfo ps = new ProcessStartInfo();
                ps.CreateNoWindow = true;
                ps.WindowStyle = ProcessWindowStyle.Hidden;
                ps.FileName = AppConfig.VideoTool;
                ps.Arguments = string.Format(" -y -i \"{0}\" -vcodec copy -acodec copy \"{1}\"",
                    file,
                    outFile);

                Process p = new Process();
                p.StartInfo = ps;
                p.Start();
                p.WaitForExit();
            }
            else
            {
                throw new FileNotFoundException("找不到文件：" + file);
            }
        }
    }
}
