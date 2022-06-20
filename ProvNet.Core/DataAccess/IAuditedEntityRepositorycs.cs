using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.DataAccess
{
    public interface IAuditedEntityRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : AuditedEntity<TId>
    {
    }
}
