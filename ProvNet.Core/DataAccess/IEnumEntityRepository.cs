using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.DataAccess
{
    public interface IEnumEntityRepository<TEntity, TId, TCode> : IReadOnlyRepository<TEntity, TId>
        where TEntity : EnumEntity<TId, TCode>

    {

    }
}
