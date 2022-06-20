using ProvNet.Core.DataAccess;
using ProvNet.Core.Model;
using ProvNet.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Service.Implementation
{
    public class ServiceWithRepository<TEntity, TId, TRepository> : IServiceWithRepository<TEntity, TId, TRepository>
            where TEntity : EntityWithId<TId>
            where TRepository : IRepository<TEntity, TId>
    {
        protected TRepository _repository { get; set; }

        public ServiceWithRepository(TRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _repository.GetAllAsync(whereCondition);
        }

        public virtual async Task<TEntity> GetFirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _repository.GetFirstOrDefaultAsync(whereCondition);
        }

        public virtual async Task<long> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public virtual async Task<long> CountAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _repository.CountAsync(whereCondition);
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            return await _repository.SaveAsync();
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            _repository.Update(entity);
            return await _repository.SaveAsync();
        }

        public virtual async Task<int> RemoveAsync(TEntity entity)
        {
            _repository.Remove(entity);
            return await _repository.SaveAsync();
        }

        public TEntity Attach(TEntity entity)
        {
            return _repository.Attach(entity);
        }

        public void Detach(TEntity entity)
        {
            _repository.Detach(entity);
        }

        public virtual async Task<long> CountByPredicatesAsync(IEnumerable<Expression<Func<TEntity, bool>>> predicates)
        {
            return await _repository.CountByPredicatesAsync(predicates);
        }

        public virtual async Task<List<TEntity>> GetByPredicatesAsync(IEnumerable<Expression<Func<TEntity, bool>>> predicates, Expression<Func<TEntity, object>> orderBy, bool isSortDescending)
        {
            return await _repository.GetByPredicatesAsync(predicates, orderBy, isSortDescending);
        }

        public virtual async Task<List<TEntity>> GetPaginatedAsync(int page, int pageSize, IEnumerable<Expression<Func<TEntity, bool>>> predicates, Expression<Func<TEntity, object>> orderBy, bool isSortDescending)
        {
            return await _repository.GetPaginatedAsync(page, pageSize, predicates, orderBy, isSortDescending);
        }
    }
}
