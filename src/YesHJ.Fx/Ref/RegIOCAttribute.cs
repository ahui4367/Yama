namespace YesHJ.Fx.Ref
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class RegIOCAttribute : Attribute
    {
        #region Fields

        private Type baseType;
        private Type subType;

        #endregion Fields

        #region Constructors

        public RegIOCAttribute(Type baseType, Type subType)
        {
            this.baseType = baseType;
            this.subType = subType;
        }

        #endregion Constructors

        #region Properties

        public Type BaseType
        {
            get
            {
                return baseType;
            }
        }

        public Type SubType
        {
            get
            {
                return subType;
            }
        }

        #endregion Properties
    }
}