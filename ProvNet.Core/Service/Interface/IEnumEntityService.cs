using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Service.Interface
{
    public interface IEnumEntityService<TEntity, TId, TCode>: IReadOnlyEntityService<TEntity>
        where TEntity : EnumEntity<TId, TCode>
    {
        Task<TEntity> GetByCodeAsync(TCode code);
        Task<Entity> GetByIdAsync(TId id);
    }
}
