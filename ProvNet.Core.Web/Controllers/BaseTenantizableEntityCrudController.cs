using AutoMapper;
using ProvNet.Core.Model;
using ProvNet.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Web.Controllers
{
    public abstract class BaseTenantizableEntityCrudController<TEntity, TId, TService, TRequestDto, TResponseDto, TListResponseDto>
        : BaseAuditedEntityCrudController<TEntity, TId, TService, TRequestDto, TResponseDto, TListResponseDto>
        where TEntity : TenantizableEntity<TId>
        where TService : ITenantizableEntityService<TEntity, TId>
    {
        public BaseTenantizableEntityCrudController(TService service, IMapper mapper) : base(service, mapper)
        {

        }
    }
}
