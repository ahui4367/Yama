using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Model
{
    public class DocumentModel
    {
        public int DocID { get; set; }

        public string DocName { get; set; }

        public string Comment { get; set; }

        public string Media { get; set; }

        public int Count { get; set; }

        public string Created { get; set; }

        public string LastModified { get; set; }
    }
}
