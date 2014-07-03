namespace Data.Access.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    using FluentData;

    using YesHJ.Fx.Error;
    using YesHJ.Fx.Extension;

    public class RepositoryImpl<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        #region Fields

        private static readonly SqlServerProvider dbProvider = new SqlServerProvider();

        private string connString = string.Empty;
        private IDbContext context;
        private ISelectBuilder<TEntity> currentCommand;
        private IUnitOfWork unitOfWork = null;

        #endregion Fields

        #region Constructors

        public RepositoryImpl()
            : this(new UnitOfWorkImpl())
        {
        }

        public RepositoryImpl(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        #endregion Constructors

        #region Properties

        public IUnitOfWork UnitOfWork
        {
            get { return unitOfWork; }
        }

        #endregion Properties

        #region Methods

        public int Add(TEntity item)
        {
            Entity entity = item as Entity;
            if (entity != null)
            {
                using (IDbCommand cmd = entity.BuildSqlCommand(DbContext(), BuildBehavior.InsertCommand))
                {
                    return cmd.ExecuteReturnLastId<int>();
                }
            }
            else
            {
                throw new NotSupportedException("Can not support repository without Entity!");
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IRepository<TEntity> GetAll()
        {
            Entity entity = new TEntity() as Entity;
            if (entity != null)
            {
                currentCommand = DbContext().Select<TEntity>(" * ")
                                            .From(string.Format(" {0} ", entity.DbTable));
            }
            else
            {
                throw new NotSupportedException("Can not support repository without Entity!");
            }
            return this;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            List<TEntity> result = new List<TEntity>();

            if (currentCommand != null)
            {
                result = currentCommand.QueryMany();
            }

            return result.GetEnumerator();
        }

        public IRepository<TEntity> GetFiltered(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            if (currentCommand == null)
                GetAll();

            if (currentCommand != null)
            {
                string whereStatement = new List<TEntity>().AsQueryable<TEntity>().GenerateSQL<TEntity>(filter);
                currentCommand = currentCommand.Where(whereStatement);
            }
            return this;
        }

        

        public IRepository<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, string orderExpr)
        {
            if (currentCommand == null)
                GetAll();

            if (currentCommand != null)
            {
                currentCommand = currentCommand.OrderBy(orderExpr)
                    .Paging(pageIndex, pageCount);
            }
            return this;
        }

        public void Remove(TEntity item)
        {
            Entity entity = item as Entity;
            if (entity != null)
            {
                using (IDbCommand cmd = entity.BuildSqlCommand(DbContext(), BuildBehavior.DeleteCommand))
                {
                    cmd.Execute();
                }
            }
            else 
            {
                throw new NotSupportedException("Can not support repository without Entity!");
            }
        }

        public void Save(TEntity item)
        {
            Entity entity = item as Entity;
            if (entity != null)
            {
                using (IDbCommand cmd = entity.BuildSqlCommand(DbContext(), BuildBehavior.UpdateCommand))
                {
                    cmd.Execute();
                }
            }
            else
            {
                throw new NotSupportedException("Can not support repository without Entity!");
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IDbContext DbContext()
        {
            if (context == null)
            {
                context = new DbContext().ConnectionStringName("fxcon", dbProvider);
            }
            return context;
        }

        #endregion Methods


        public IRepository<TEntity> GetFiltered(string expr)
        {
            if (currentCommand == null)
                GetAll();
            
            if (currentCommand != null && !string.IsNullOrEmpty(expr))
            {
                currentCommand = currentCommand.Where(expr);
            }
            return this;
        }


        public IRepository<TEntity> Parameter(string name, object value)
        {
            if (currentCommand != null)
            {
                currentCommand.Parameter(name, value);
            }
            return this;
        }
    }
}