using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Model
{
    public class VideoSearchModel : PagingModel
    {
        public int SearchType { get; set; }

        public string SearchText { get; set; }

        public override void ValidateParameters()
        {
            base.ValidateParameters();
            Total = 0;
        }
    }
}
