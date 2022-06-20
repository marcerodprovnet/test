using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Service.Interface
{
    public interface IReadOnlyEntityService<TEntity>: IService
        where TEntity : IEntity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> whereCondition);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereCondition);
        Task<long> CountAsync();
        Task<long> CountAsync(Expression<Func<TEntity, bool>> whereCondition);
    }
}
