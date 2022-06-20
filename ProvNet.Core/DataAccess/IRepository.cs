using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.DataAccess
{
    public interface IRepository<TEntity, TId>
      where TEntity : EntityWithId<TId>
    {
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition);
        Task<TEntity> GetFirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition);
        Task<TEntity> GetByIdAsync(TId id);
        Task<long> CountAsync();
        Task<long> CountAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        TEntity Attach(TEntity entity);
        void Detach(TEntity entity);
        IQueryable<TEntity> GetQueryableAsync();
        void BeginTransactionAsync();
        void CommitTransactionAsync();
        void RollbackTransactionAsync();
        Task<int> SaveAsync();

        Task<long> CountByPredicatesAsync(IEnumerable<Expression<Func<TEntity, bool>>> predicates);

        IQueryable<TEntity> GetByPredicatesQueryable(
            IEnumerable<Expression<Func<TEntity, bool>>> predicates,
            Expression<Func<TEntity, object>> orderBy,
            bool isSortDescending);

        IQueryable<TEntity> GetPaginatedQueryable(
            int page,
            int pageSize,
            IEnumerable<Expression<Func<TEntity, bool>>> predicates,
            Expression<Func<TEntity, object>> orderBy,
            bool isSortDescending);

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
