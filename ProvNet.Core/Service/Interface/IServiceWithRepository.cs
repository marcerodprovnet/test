using ProvNet.Core.DataAccess;
using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Service.Interface
{
    public interface IServiceWithRepository<TEntity, TId, TRepository> : IEntityService<TEntity>
        where TEntity : EntityWithId<TId>
        where TRepository : IRepository<TEntity, TId>
    {
    }
}
