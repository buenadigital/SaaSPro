using System;
using System.Collections.Generic;
using System.Linq;

namespace SaaSPro.Common
{
    public static class RepositoryExtensions
    {
        public static IEnumerable<TEntity> Fetch<TId, TEntity>(this IRepository<TId, TEntity> repository,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> query) where TEntity : IEntity<TId>
        {
            return query(repository.Query()).ToList();
        }
    }
}