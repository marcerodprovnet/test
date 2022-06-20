using ProvNet.Core.DataAccess;
using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.EntityFramework.Interfaces
{
    public interface ITenantizableEntityRepository<TEntity, TId, TDbContext>: IDbContextRepository<TEntity, TId,TDbContext>,ITenantizableEntityRepository<TEntity, TId>
        where TEntity : TenantizableEntity<TId>
        where TDbContext : IDbContext
    {
    }
}
