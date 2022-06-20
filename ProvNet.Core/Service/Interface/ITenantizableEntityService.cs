using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Service.Interface
{
    public interface ITenantizableEntityService<TEntity, TId> : IAuditedEntityService<TEntity, TId>
        where TEntity: TenantizableEntity<TId>
    {
    }
}
