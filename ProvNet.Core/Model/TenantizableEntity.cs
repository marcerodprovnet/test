using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Model
{
    public class TenantizableEntity<TId> : AuditedEntity<TId>, ITenantizableEntity<TId>
    {
        public int TenantId { get; set; }
    }
}
