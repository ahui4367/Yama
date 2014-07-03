namespace View.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RoleModel
    {
        #region Properties

        public int RoleID { get; set; }

        public string Description
        {
            get;
            set;
        }

        public string RoleName
        {
            get;
            set;
        }

        public string Created
        {
            get;
            set;
        }

        public string LastModified
        {
            get;
            set;
        }

        #endregion Properties
    }
}