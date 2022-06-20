using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Model
{
    public abstract class EnumEntity<TId, TCode> : EntityWithId<TId>, IEnumEntity<TId, TCode>
    {
         public abstract TCode Code { get; set; }
        [StringLength(100)]
        public string Translation { get; set; }
    }
}
