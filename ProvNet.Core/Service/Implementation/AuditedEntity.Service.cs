using ProvNet.Core.Authentication.Interfaces;
using ProvNet.Core.DataAccess;
using ProvNet.Core.Model;
using ProvNet.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Service.Implementation
{
    public class AuditedEntityService<TEntity, TId, TRepository> : EntityWithIdService<TEntity, TId, TRepository>, IAuditedEntityService<TEntity, TId>
     where TEntity : AuditedEntity<TId>
     where TRepository : IAuditedEntityRepository<TEntity, TId>
    {
        public AuditedEntityService(TRepository repository, IUserContext userContext)
            : base(repository)
        {
            UserContext = userContext;
        }

        protected IUserContext UserContext { get; set; }

        public override async Task<int> AddAsync(TEntity entity)
        {
            entity.CreationDate = DateTime.UtcNow;
            entity.CreationUser = UserContext.User.UserId;
            entity.LastUpdateDate = DateTime.UtcNow;
            entity.LastUpdateUser = UserContext.User.UserId;

            return await base.AddAsync(entity);
        }

        public override async Task<int> UpdateAsync(TEntity entity)
        {
            entity.LastUpdateDate = DateTime.UtcNow;
            entity.LastUpdateUser = UserContext.User.UserId;

            return await base.UpdateAsync(entity);
        }

        public override async Task<int> RemoveAsync(TEntity entity)
        {
            entity.DeletionDate = DateTime.UtcNow;
            entity.DeletetionUser = UserContext.User.UserId;

            return await base.UpdateAsync(entity);
        }
        public override async Task<bool> RemoveByIdAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                return await RemoveAsync(entity) == 1;
            }
            return false;
        }
    }
}
