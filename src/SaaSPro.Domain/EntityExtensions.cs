using System;
using System.Collections.Generic;
using System.Linq;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Domain
{
    public static class EntityExtensions
    {
        private static Action<object> emptyAction = o => { };
        
        /// <summary>
        /// Maps any entities found by comparing the entity Id against the 
        /// provided <param name="entityIds"/> to the appropriate action.
        /// </summary>
        public static void Map<TEntity>(
            this IEnumerable<TEntity> collection, 
            IEnumerable<Guid> entityIds, 
            Action<TEntity> foundAction = null, 
            Action<TEntity> notFoundAction = null) where TEntity : Entity
        {
            Ensure.Argument.NotNull(collection, "collection");
            
            foreach (var item in collection)
            {
                if (entityIds != null && entityIds.Contains(item.Id))
                {
                    (foundAction ?? emptyAction)(item);
                }
                else
                {
                    (notFoundAction ?? emptyAction)(item);
                }
            }
        }

		public static T ParseEnum<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value, true);
		}
	}
}
