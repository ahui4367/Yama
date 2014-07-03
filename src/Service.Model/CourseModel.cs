using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Model
{
    public class CourseModel
    {
        public Int32 Cid { get; set; }

        public String CourseName { get; set; }

        public String Category { get; set; }

        public Int32 Rank { get; set; }

        public Int32 Limit { get; set; }

        public String Type { get; set; }

        public String Media { get; set; }

        public String Created { get; set; }

        public String Lastmodified { get; set; }

        public Boolean Active { get; set; }   
    }
}
