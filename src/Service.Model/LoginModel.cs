namespace View.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LoginModel
    {
        #region Properties

        public string Password
        {
            get;
            set;
        }

        public string UserCode
        {
            get;
            set;
        }

        public string Captcha
        {
            get;
            set;
        }

        public string Error
        {
            get;
            set;
        }

        #endregion Properties
    }
}