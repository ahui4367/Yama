namespace YesHJ.Fx.Ref
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
    public class WarmupAttribute : Attribute
    {
        #region Fields

        private MethodBase method = MethodInfo.GetCurrentMethod();
        private string name;

        #endregion Fields

        #region Constructors

        public WarmupAttribute(string name)
        {
            this.name = name;
        }

        #endregion Constructors

        #region Properties

        public MethodBase Method
        {
            get
            {
                return method;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        #endregion Properties
    }
}