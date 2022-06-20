using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Service.Interface
{
    public interface IEntityWithIdService<TEntity, TId> : IEntityService<TEntity>
        where TEntity : EntityWithId<TId>
    {
        Task<TEntity> GetByIdAsync(TId id);
        Task<bool> RemoveByIdAsync(TId id);
    }
}
