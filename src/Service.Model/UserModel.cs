namespace View.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DbModel.AspnetDb;

    public class UserModel
    {
        #region Properties

        public Int32 Uid { get; set; }

        public String Ucode { get; set; }

        public String Uname { get; set; }

        public String Upass { get; set; }

        public String Ucard { get; set; }

        public String Utag { get; set; }

        public String Email { get; set; }

        public String Mobile { get; set; }

        public String Im { get; set; }

        public Int32 Oid { get; set; }

        public String Organ { get; set; }

        public String Comment { get; set; }

        public string Created { get; set; }

        public string Lastmodified { get; set; }

        public Boolean Active { get; set; }    
        
        #endregion Properties
    }
}