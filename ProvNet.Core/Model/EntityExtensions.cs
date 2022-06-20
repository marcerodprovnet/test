using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Model
{
    public static class EntityExtensions
    {
        public static IQueryable<TEntity> AddSecurityFilter<TEntity, TId>(this IQueryable<TEntity> queryable) where TEntity : TenantizableEntity<TId>
        {
            return queryable.AddDeletionFilter<TEntity, TId>();
        }
        public static IQueryable<TEntity> AddDeletionFilter<TEntity, TId>(this IQueryable<TEntity> queryable) where TEntity : AuditedEntity<TId>
        {
            queryable.Where(x => x.DeletionDate == null);
            return queryable;
        }
    }
}
