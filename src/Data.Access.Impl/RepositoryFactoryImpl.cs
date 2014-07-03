namespace Data.Access.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Data.Access.Impl;

    public class RepositoryFactoryImpl : RepositoryFactory
    {
        #region Methods

        public override IRepository<TEntity> Create<TEntity>(IUnitOfWork uow)
        {
            return new RepositoryImpl<TEntity>();
        }

        #endregion Methods
    }
}