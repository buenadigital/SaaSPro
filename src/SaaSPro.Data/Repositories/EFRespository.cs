using System;
using System.Data.Entity.Migrations;
using System.Linq;
using SaaSPro.Common;
using SaaSPro.Common.Helpers;
using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public  class EFRespository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly EFDbContext _dbContext;

        public EFRespository(EFDbContext dbContext)
        {
            Ensure.Argument.NotNull(dbContext, "_dbContext");
            _dbContext = dbContext;
        }

        public TEntity Get(Guid id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void Add(TEntity entity)
        {
            Ensure.Argument.NotNull(entity, "entity");
            _dbContext.Set<TEntity>().Add(entity);
	        Commit();
        }

        public void Update(TEntity entity)
        {
            Ensure.Argument.NotNull(entity, "entity");
            _dbContext.Set<TEntity>().AddOrUpdate(entity);
	        Commit();
        }

        public void Delete(TEntity entity)
        {
            Ensure.Argument.NotNull(entity, "entity");
            _dbContext.Set<TEntity>().Remove(entity);
	        Commit();
        }
        
        public IPagedList<TEntity> FetchPaged(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, int pageIndex, int pageSize)
        {
            Ensure.Argument.NotNull(query, "query");
			return FetchPagedResults(query(Query()), pageIndex, pageSize);
		}

        public IQueryable<TEntity> Query()
        {
			return _dbContext.Set<TEntity>().AsQueryable();
        }
        
		private IPagedList<TEntity> FetchPagedResults(IQueryable<TEntity> query, int pageIndex, int pageSize)
        {
            var futureCount = query.Count();

            var isOrdered = query.Expression.Type == typeof(IOrderedQueryable<TEntity>);
            if (!isOrdered)
                query = query.OrderBy(x => x.Id);

            return new PagedList<TEntity>(query.Skip(pageSize * pageIndex).Take(pageSize),
                pageIndex, pageSize, futureCount);
        }

	    private void Commit()
	    {
		    _dbContext.SaveChanges();
	    }
    }
}