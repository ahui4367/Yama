using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Model
{
    public class VideoModel
    {
        public int VideoID { get; set; }

        public string VideoName { get; set; }

        public string Comment { get; set; }

        public string Media { get; set; }

        public string Created { get; set; }

        public string LastModified { get; set; }
    }
}
