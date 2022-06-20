using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.DataAccess
{
    public interface IReadOnlyRepository<TEntity, TId>
        where TEntity : EntityWithId<TId>
    {
        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetAllAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition);

        Task<TEntity> GetFirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition);

        Task<TEntity> GetByIdAsync(TId id);

        Task<long> CountAsync();
        Task<long> CountAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition);

        IQueryable<TEntity> GetQueryableAsync();
    }
}
