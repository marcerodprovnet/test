

namespace ProvNet.Core.EntityFramework.Interfaces
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System.Threading.Tasks;

    public interface IDbContext
    {
        Task<int> SaveChangesAsync();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Task RollbackTransactionAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
    }
}
