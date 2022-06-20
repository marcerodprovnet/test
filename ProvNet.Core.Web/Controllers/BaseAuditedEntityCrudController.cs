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
    public abstract class BaseAuditedEntityCrudController<TEntity, TId, TService, TRequestDto, TResponseDto, TListResponseDto>
        : BaseEntityWithServiceCrudController<TEntity, TId,TService,TRequestDto, TResponseDto, TListResponseDto>
        where TEntity : AuditedEntity<TId>
        where TService : IAuditedEntityService<TEntity, TId>
    {
        public BaseAuditedEntityCrudController (TService service, IMapper mapper) : base(service, mapper)
        {

        }
    }
}
