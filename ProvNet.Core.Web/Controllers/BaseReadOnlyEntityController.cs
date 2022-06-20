using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProvNet.Core.Model;
using ProvNet.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Web.Controllers
{
    public class BaseReadOnlyEntityController<TEntity, TId, TService, TResponseDto> : BaseController
        where TEntity : EntityWithId<TId>
        where TService : IReadOnlyEntityService<TEntity>
    {
        protected readonly TService _service;
        protected readonly IMapper _mapper;

        public BaseReadOnlyEntityController(TService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IList<TResponseDto>> GetAll()
        {
            return _mapper.Map<IList<TResponseDto>>(await _service.GetAllAsync());
        }
    }
}
