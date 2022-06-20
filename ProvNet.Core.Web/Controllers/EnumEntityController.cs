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
    public class EnumEntityController<TEntity, TId, TCode, TService, TResponseDto> : BaseReadOnlyEntityController<TEntity, TId, TService, TResponseDto>
        where TEntity : EnumEntity<TId, TCode>
        where TService : IEnumEntityService<TEntity, TId, TCode>
    {

        public EnumEntityController(TService service, IMapper mapper) : base(service, mapper) { }

        [HttpGet]
        [Route("code/{code}")]
        public async Task<ActionResult<TResponseDto>> GetByCode(TCode code)
        {
            var data = await _service.GetByCodeAsync(code);
            if (data == null)
                return NotFound();
            return _mapper.Map<TResponseDto>(data);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TResponseDto>> GetById([FromRoute] TId id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
                return NotFound();
            return _mapper.Map<TResponseDto>(data);
        }
    }
}
