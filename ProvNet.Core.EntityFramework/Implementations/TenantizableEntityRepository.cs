using Microsoft.EntityFrameworkCore;
using ProvNet.Core.DataAccess;
using ProvNet.Core.EntityFramework.Interfaces;
using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.EntityFramework.Implementations
{
    public class TenantizableEntityRepository<TEntity, TId, TDbContext> : AuditedEntityRepository<TEntity, TId, TDbContext>, ITenantizableEntityRepository<TEntity, TId>
        where TEntity : TenantizableEntity<TId>
        where TDbContext : IDbContext
    {
        public TenantizableEntityRepository(TDbContext dbcontext) : base(dbcontext)
        {
        }

        public override async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>()
                .AsQueryable<TEntity>()
                .AddSecurityFilter<TEntity, TId>()
                .ToListAsync();
        }

        public override async Task<List<TEntity>> GetAllAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _context.Set<TEntity>()
                .AsQueryable<TEntity>()
                .AddSecurityFilter<TEntity, TId>()
                .Where(whereCondition)
                .ToListAsync();
        }

        public override async Task<TEntity> GetFirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _context.Set<TEntity>()
                .AsQueryable<TEntity>()
                .AddSecurityFilter<TEntity, TId>()
                .FirstOrDefaultAsync(whereCondition);
        }

        public override async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _context.Set<TEntity>()
                .AsQueryable<TEntity>()
                .AddSecurityFilter<TEntity, TId>()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public override async Task<long> CountAsync()
        {
            return await _context.Set<TEntity>()
                .AsQueryable<TEntity>()
                .AddSecurityFilter<TEntity, TId>()
                .LongCountAsync();
        }

        public override async Task<long> CountAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _context.Set<TEntity>()
                .AsQueryable<TEntity>()
                .AddSecurityFilter<TEntity, TId>()
                .Where(whereCondition)
                .LongCountAsync();
        }

        public override IQueryable<TEntity> GetByPredicatesQueryable(IEnumerable<Expression<Func<TEntity, bool>>> predicates, Expression<Func<TEntity, object>> orderBy, bool isSortDescending)
        {
            return base.GetByPredicatesQueryable(predicates, orderBy, isSortDescending)
                .AddSecurityFilter<TEntity, TId>();
        }
    }
}
