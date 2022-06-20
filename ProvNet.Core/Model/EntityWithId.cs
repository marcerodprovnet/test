using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Model
{
    public class EntityWithId<TId> : Entity
    {
        [Key]
        public TId Id { get; set; }
    }
}
