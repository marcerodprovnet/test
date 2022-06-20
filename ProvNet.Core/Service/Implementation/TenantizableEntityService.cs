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
    public class TenantizableEntityService<TEntity, TId, TRepository> : AuditedEntityService<TEntity, TId, TRepository>, ITenantizableEntityService<TEntity, TId>
        where TEntity : TenantizableEntity<TId>
        where TRepository : ITenantizableEntityRepository<TEntity, TId>
    {
        public TenantizableEntityService(
            TRepository repository,
            IUserContext userContext)
                : base(repository, userContext)
        {
        }

        public override async Task<int> AddAsync(TEntity entity)
        {
            entity.TenantId = UserContext.User.TenantId;
            return await base.AddAsync(entity);
        }
    }
}
