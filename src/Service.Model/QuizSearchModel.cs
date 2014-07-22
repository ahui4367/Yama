using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Model
{
    public class QuizSearchModel : PagingModel
    {
        public int CourseID { get; set; }

        public string SearchText { get; set; }

        public int SearchType { get; set; }
    }
}
