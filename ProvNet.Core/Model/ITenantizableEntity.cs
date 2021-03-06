using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Model
{
    public interface ITenantizableEntity<TId> : IEntityWithId<TId>
    {
        int TenantId { get; set; }
    }
}
