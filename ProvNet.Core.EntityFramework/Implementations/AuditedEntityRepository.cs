

namespace ProvNet.Core.EntityFramework.Implementations
{
    using Microsoft.EntityFrameworkCore;
    using ProvNet.Core.DataAccess;
    using ProvNet.Core.EntityFramework.Interfaces;
    using ProvNet.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class AuditedEntityRepository<TEntity, TId, TDbContext> : DbContextRepository<TEntity, TId, TDbContext>, IAuditedEntityRepository<TEntity, TId>
       where TEntity : AuditedEntity<TId>
       where TDbContext : IDbContext
    {
        public AuditedEntityRepository(TDbContext dbcontext) : base (dbcontext)
        {
        }

        public override async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>()
                .AsQueryable<TEntity>()
                .AddDeletionFilter<TEntity, TId>()
                .ToListAsync();
        }

        public override async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _context.Set<TEntity>()
                .AsQueryable<TEntity>()
                .AddDeletionFilter<TEntity, TId>()
                .Where(whereCondition)
                .ToListAsync();
        }

        public override async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _context.Set<TEntity>()
                .AsQueryable<TEntity>()
                .AddDeletionFilter<TEntity, TId>()
                .FirstOrDefaultAsync(whereCondition);
        }

        public override async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _context.Set<TEntity>()
                .AsQueryable<TEntity>()
                .AddDeletionFilter<TEntity, TId>()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public override async Task<long> CountAsync()
        {
            return await _context.Set<TEntity>()
                .AsQueryable<TEntity>()
                .AddDeletionFilter<TEntity, TId>()
                .LongCountAsync();
        }

        public override async Task<long> CountAsync(Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _context.Set<TEntity>()
                .AsQueryable<TEntity>()
                .AddDeletionFilter<TEntity, TId>()
                .Where(whereCondition)
                .LongCountAsync();
        }

        public override IQueryable<TEntity> GetByPredicatesQueryable(IEnumerable<Expression<Func<TEntity, bool>>> predicates, Expression<Func<TEntity, object>> orderBy, bool isSortDescending)
        {
            return base.GetByPredicatesQueryable(predicates, orderBy, isSortDescending)
                .AddDeletionFilter<TEntity, TId>();
        }
    }
}
