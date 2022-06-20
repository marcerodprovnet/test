using Microsoft.EntityFrameworkCore;
using ProvNet.Core.EntityFramework.Interfaces;
using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.EntityFramework.Implementations
{
    public class DbContextReadOnlyRepository<TEntity, TId, TDbContext> : IDbContextReadOnlyRepository<TEntity, TId, TDbContext>
        where TEntity : EntityWithId<TId>
        where TDbContext : IDbContext
    {
        private TDbContext _context;

        public DbContextReadOnlyRepository(TDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _context.Set<TEntity>().Where(whereCondition).ToListAsync();
        }

        public virtual async Task<TEntity> GetFirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(whereCondition);
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<long> CountAsync()
        {
            return await _context.Set<TEntity>().LongCountAsync();
        }

        public virtual async Task<long> CountAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _context.Set<TEntity>().Where(whereCondition).LongCountAsync();
        }

        public IQueryable<TEntity> GetQueryableAsync()
        {
            return _context.Set<TEntity>().AsQueryable<TEntity>();
        }
    }
}
