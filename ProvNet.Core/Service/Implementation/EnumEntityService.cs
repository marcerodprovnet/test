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
    public class EnumEntityService<TEntity, TId, TCode, TRepository> : ReadOnlyServiceWithRepository<TEntity, TId, TRepository>, IEnumEntityService<TEntity, TId, TCode>
           where TEntity : EnumEntity<TId, TCode>
           where TRepository : IEnumEntityRepository<TEntity, TId, TCode>
    {
        public EnumEntityService(
            TRepository repository)
                : base(repository)
        {
        }

        public async Task<TEntity> GetByCodeAsync(TCode code)
        {
            return await _repository.GetFirstOrDefaultAsync(x => x.Code.Equals(code));
        }

        public async Task<Entity> GetByIdAsync(TId id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
