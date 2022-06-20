using ProvNet.Core.DataAccess;
using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.EntityFramework.Interfaces
{
    public interface IDbContextReadOnlyRepository<TEntity, TId, TDbContext>: IReadOnlyRepository<TEntity, TId>
        where TEntity :EntityWithId<TId>
        where TDbContext : IDbContext
    {
    }
}
