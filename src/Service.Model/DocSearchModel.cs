using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Model
{
    public class DocSearchModel : PagingModel
    {
        public int SearchType { get; set; }

        public string SearchText { get; set; }
    }
}
