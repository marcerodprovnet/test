using ProvNet.Core.DataAccess;
using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.EntityFramework.Interfaces
{
    public interface IAuditedEntityRepository<TEntity, TId, TDbContext> : IDbContextRepository<TEntity, TId, TDbContext>,IAuditedEntityRepository<TEntity, TId>
        where TEntity : AuditedEntity<TId>
        where TDbContext : IDbContext
    {
    }
}
