using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProvNet.Core.Model;
using ProvNet.Core.Service.Interface;
using ProvNet.Core.Web.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Web.Controllers
{
    public abstract class BaseEntityWithServiceCrudController<TEntity, TId, TService, TRequestDto, TResponseDto, TListResponseDto> : BaseController
        where TEntity : EntityWithId<TId>
        where TService : IEntityWithIdService<TEntity, TId>
    {
        protected readonly TService _service;
        protected readonly IMapper _mapper;

        protected abstract IDictionary<string, string> FiltrableFields { get; }
        protected abstract IDictionary<string, string> SortableFields { get; }

        public BaseEntityWithServiceCrudController(TService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")] 
        public async Task<IList<TListResponseDto>> GetAll()
        {
            return _mapper.Map<IList<TListResponseDto>>(await _service.GetAllAsync());
        }

        [HttpPost]
        [Route("paginated")]
        public async Task<IList<TListResponseDto>> GetAllPaginated([FromBody] PaginateRequestDto requestDto)
        {
            var defaultPage = 1;
            var defaultPageSize = 25;

            var page = requestDto.Page ?? defaultPage;
            var pageSize = requestDto.PageSize ?? defaultPageSize;

            if (page < 1)
            {
                page = defaultPage;
            }

            if (pageSize < 1 || pageSize > 100)
            {
                pageSize = defaultPageSize;
            }

            var helper = new ListQueryHelper();

            var predicates = helper.GetFilters<TEntity>(requestDto.Filters, FiltrableFields);
            var orderBy = helper.GetOrderBy<TEntity>(requestDto, SortableFields);
            var isSortdDescending = requestDto.SortDirection == SortDirection.Descending;

            return _mapper.Map<IList<TListResponseDto>>(await _service.GetPaginatedAsync(page, pageSize, predicates, orderBy, isSortdDescending));
        }

        [HttpPost]
        [Route("count")]
        public async Task<long> GetFilteredCount([FromBody] IList<FilterRequestDto> requestDto)
        {
            var helper = new ListQueryHelper();
            var predicates = helper.GetFilters<TEntity>(requestDto, FiltrableFields);

            return await _service.CountByPredicatesAsync(predicates);
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

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromBody] TRequestDto requestDto)
        {
            var entity = _mapper.Map<TEntity>(requestDto);
            await _service.AddAsync(entity);
            return Created(string.Empty, _mapper.Map<TResponseDto>(entity));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] TId id, [FromBody] TRequestDto requestDto)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                _mapper.Map(requestDto, entity);
                await _service.UpdateAsync(entity);
                return Ok(_mapper.Map<TResponseDto>(entity));
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] TId id)
        {
            var deleted = await _service.RemoveByIdAsync(id);
            // TODO: Review NotFound Logic
            if (!deleted)
                return NotFound();
            return Ok();
        }
    }
}
