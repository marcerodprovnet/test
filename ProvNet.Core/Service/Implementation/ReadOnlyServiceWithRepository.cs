using ProvNet.Core.DataAccess;
using ProvNet.Core.Model;
using ProvNet.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Service.Implementation
{
    public class ReadOnlyServiceWithRepository<TEntity, TId, TRepository> : IReadOnlyServiceWithRepository<TEntity, TId, TRepository>
           where TEntity : EntityWithId<TId>
           where TRepository : IReadOnlyRepository<TEntity, TId>
    {
        protected TRepository _repository { get; set; }

        public ReadOnlyServiceWithRepository(TRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _repository.GetAllAsync(whereCondition);
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _repository.GetFirstOrDefaultAsync(whereCondition);
        }

        public async Task<long> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public async Task<long> CountAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _repository.CountAsync(whereCondition);
        }
    }
}
