using System;
using System.Linq;
using System.Linq.Expressions;
using SaaSPro.Common.Helpers;
using SaaSPro.Common;
using SaaSPro.Domain;

namespace SaaSPro.Services.Helpers
{
    public static class QueryExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool criteria, Expression<Func<T, bool>> predicate)
        {
            if (criteria)
            {
                source = source.Where(predicate);
            }

            return source;
        }
        
        public static ReferenceList GetBySystemName(this IRepository<ReferenceList> repo, string systemName)
        {
            Ensure.Argument.NotNullOrEmpty(systemName, "systemName");
            return repo.Query().FirstOrDefault(x => x.SystemName == systemName);
        }
    }
}