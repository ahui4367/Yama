
namespace Data.Access
{
    public abstract class RepositoryFactory
    {
        #region Methods

        public virtual IRepository<TEntity> Create<TEntity>()
            where TEntity : class, new()
        {
            return Create<TEntity>(null);
        }

        public abstract IRepository<TEntity> Create<TEntity>(IUnitOfWork uow)
            where TEntity : class, new();

        #endregion Methods
    }
}