using Microsoft.EntityFrameworkCore;
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
    public class DbContextRepository<TEntity, TId, TDbContext> : IDbContextRepository<TEntity, TId, TDbContext>
        where TEntity : EntityWithId<TId>
        where TDbContext : IDbContext
    {
        protected TDbContext _context;

        public DbContextRepository(TDbContext dbcontext)
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

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public TEntity Attach(TEntity entity)
        {
            return _context.Set<TEntity>().Attach(entity).Entity;
        }

        public void Detach(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public IQueryable<TEntity> GetQueryableAsync()
        {
            return _context.Set<TEntity>().AsQueryable<TEntity>();
        }

        public void BeginTransactionAsync()
        {
            _context.BeginTransactionAsync();
        }

        public void CommitTransactionAsync()
        {
            _context.CommitTransactionAsync();
        }

        public void RollbackTransactionAsync()
        {
            _context.RollbackTransactionAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual IQueryable<TEntity> GetByPredicatesQueryable(
            IEnumerable<Expression<Func<TEntity, bool>>> predicates,
            Expression<Func<TEntity, object>> orderBy,
            bool isSortDescending)
        {
            var queryable = _context.Set<TEntity>().AsQueryable();
            if (predicates != null)
            {
                foreach (var predicate in predicates)
                {
                    queryable = queryable.Where(predicate);
                }
            }

            if (orderBy != null)
            {
                if (isSortDescending)
                {
                    queryable = queryable.OrderByDescending(orderBy);
                }
                else
                {
                    queryable = queryable.OrderBy(orderBy);
                }
            }

            return queryable;
        }

        public virtual IQueryable<TEntity> GetPaginatedQueryable(int page, int pageSize, IEnumerable<Expression<Func<TEntity, bool>>> predicates, Expression<Func<TEntity, object>> orderBy, bool isSortDescending)
        {
            return GetByPredicatesQueryable(predicates, orderBy, isSortDescending)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
        }

        public virtual async Task<long> CountByPredicatesAsync(IEnumerable<Expression<Func<TEntity, bool>>> predicates)
        {
            return await GetByPredicatesQueryable(predicates, null, false).CountAsync();
        }

        public virtual async Task<List<TEntity>> GetByPredicatesAsync(
            IEnumerable<Expression<Func<TEntity, bool>>> predicates,
            Expression<Func<TEntity, object>> orderBy,
            bool isSortDescending)
        {
            return await GetByPredicatesQueryable(predicates, orderBy, isSortDescending).ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetPaginatedAsync(
            int page,
            int pageSize,
            IEnumerable<Expression<Func<TEntity, bool>>> predicates,
            Expression<Func<TEntity, object>> orderBy,
            bool isSortDescending)
        {
            return await GetPaginatedQueryable(page, pageSize, predicates, orderBy, isSortDescending).ToListAsync();
        }
    }
}
