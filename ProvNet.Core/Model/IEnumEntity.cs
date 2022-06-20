using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Model
{
    public interface IEnumEntity<TId, TCode> : IEntityWithId<TId>
    {
        TCode Code { get; set; }
        string Translation { get; set; }
    }
}
