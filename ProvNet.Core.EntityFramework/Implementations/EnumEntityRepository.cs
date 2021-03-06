using ProvNet.Core.DataAccess;
using ProvNet.Core.EntityFramework.Interfaces;
using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.EntityFramework.Implementations
{
    public class                                                            EnumEntityRepository<TEntity, TId, TCode, TDbContext> : DbContextRepository<TEntity, TId, TDbContext>, IEnumEntityRepository<TEntity, TId, TCode>
        where TEntity : EnumEntity<TId, TCode>
        where TDbContext : IDbContext
    {
        public EnumEntityRepository(TDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
