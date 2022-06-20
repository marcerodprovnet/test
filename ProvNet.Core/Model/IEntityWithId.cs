using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Model
{
    public interface IEntityWithId<TId> : IEntity
    {
        TId Id { get; set; }
    }
}
