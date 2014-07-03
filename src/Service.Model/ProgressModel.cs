using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Model
{
    public class ProgressModel
    {
        public int UserID { get; set; }

        public int CourseID { get; set; }

        public string Corrects { get; set; }

        public string Incorrects { get; set; }
    }
}
