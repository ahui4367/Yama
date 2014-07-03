using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace View.Model
{
    public class PagingModel
    {
        public PagingModel()
        {
            Parameters = new Dictionary<string, string>();
        }

        public int Total { get; set; }

        public int Page { get; set; }

        public int Rows { get; set; }

        public IDictionary<string, string> Parameters { get; set; }

        public virtual void ValidateParameters()
        {
            Page = Page < 1 ? 1 : Page;
            Rows = Rows < 10 ? 10 : Rows;
            Total = Total < 0 ? 0 : Total;
        }
    }
}
