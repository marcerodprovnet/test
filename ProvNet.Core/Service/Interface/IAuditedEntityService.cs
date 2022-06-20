using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Service.Interface
{
    public interface IAuditedEntityService<TEntity, TId> : IEntityWithIdService<TEntity, TId>
        where TEntity : AuditedEntity<TId>
    {
    }
}
