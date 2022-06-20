using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Model
{
    using System;
    public class AuditedEntity<TId> : EntityWithId<TId>, IAuditedEntity<TId>
    {
        public string CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public string DeletetionUser { get; set; }
    }
}
