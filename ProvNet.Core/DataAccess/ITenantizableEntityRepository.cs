using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.DataAccess
{
    public interface ITenantizableEntityRepository<TEntity, TId> : IAuditedEntityRepository<TEntity, TId>
        where TEntity : TenantizableEntity<TId>
    {
    }
}
