using System;
using System.Linq;

namespace SaaSPro.Common
{
    public interface IRepository<TId, TEntity> where TEntity : IEntity<TId>
    {
        TEntity Get(TId id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IPagedList<TEntity> FetchPaged(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, int pageIndex, int pageSize);
        IQueryable<TEntity> Query();
    }

    public interface IRepository<TEntity> : IRepository<Guid, TEntity> where TEntity : IEntity<Guid>
    {

    }
}
