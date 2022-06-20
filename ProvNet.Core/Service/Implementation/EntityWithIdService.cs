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
    public class EntityWithIdService<TEntity, TId, TRepository> : ServiceWithRepository<TEntity, TId, TRepository>, IEntityWithIdService<TEntity, TId>
      where TEntity : EntityWithId<TId>
      where TRepository : IRepository<TEntity, TId>
    {
        public EntityWithIdService(TRepository repository) : base(repository)
        {
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _repository.GetFirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<bool> RemoveByIdAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }
            else
            {
                _repository.Remove(entity);
                return true;
            }
        }
    }
}
