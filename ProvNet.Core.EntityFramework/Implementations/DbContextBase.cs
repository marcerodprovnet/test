using Microsoft.EntityFrameworkCore;
using ProvNet.Core.EntityFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.EntityFramework.Implementations
{
    public class DbContextBase : DbContext, IDbContext
    {
        public DbContextBase()
        {
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public Task RollbackTransactionAsync()
        {
            return Database.CurrentTransaction.RollbackAsync();
        }
        public Task BeginTransactionAsync()
        {
            return Database.BeginTransactionAsync();
        }

        public Task CommitTransactionAsync()
        {
            return Database.CurrentTransaction.CommitAsync();
        }
    }
}
