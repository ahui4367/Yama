namespace Data.Access.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FluentData;

    public abstract class Entity
    {
        #region Properties

        public abstract string DbTable
        {
            get;
        }

        #endregion Properties

        #region Methods

        public abstract IDbCommand BuildSqlCommand(IDbContext context, BuildBehavior behavior);

        #endregion Methods
    }
}