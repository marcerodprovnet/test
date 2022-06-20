using ProvNet.Core.DataAccess;
using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.EntityFramework.Interfaces
{
    public interface IEnumEntityRepository<TEntity, TId, TCode, TDbContext> : IDbContextRepository<TEntity, TId, TDbContext>, IEnumEntityRepository<TEntity, TId, TCode>
        where TEntity : EnumEntity<TId, TCode>
        where TDbContext : IDbContext
    {
    }
}
