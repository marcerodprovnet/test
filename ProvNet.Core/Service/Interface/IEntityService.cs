using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Service.Interface
{
    public interface IEntityService<TEntity> : IService
   where TEntity : IEntity
    {
        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> whereCondition);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereCondition);

        Task<long> CountAsync();

        Task<long> CountAsync(Expression<Func<TEntity, bool>> whereCondition);

        Task<int> AddAsync(TEntity entity);

        Task<int> UpdateAsync(TEntity entity);

        Task<int> RemoveAsync(TEntity entity);

        TEntity Attach(TEntity entity);
        void Detach(TEntity entity);

        Task<long> CountByPredicatesAsync(IEnumerable<Expression<Func<TEntity, bool>>> predicates);

        Task<List<TEntity>> GetByPredicatesAsync(
            IEnumerable<Expression<Func<TEntity, bool>>> predicates,
            Expression<Func<TEntity, object>>
            orderBy,
            bool isSortDescending);

        Task<List<TEntity>> GetPaginatedAsync(
            int page,
            int pageSize,
            IEnumerable<Expression<Func<TEntity, bool>>> predicates,
            Expression<Func<TEntity, object>> orderBy,
            bool isSortDescending);
    }
}
