using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Model
{
    using System;
    public interface IAuditedEntity<TId> : IEntityWithId<TId>
    {
        string CreationUser { get; set; }
        DateTime CreationDate { get; set; }
        string LastUpdateUser { get; set; }
        DateTime LastUpdateDate { get; set; }

    }
}
