using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Web.Pagination
{
    public class PaginateRequestDto
    {
        public IList<FilterRequestDto> Filters  { get; set; }
        public string SortField { get; set; }
        public SortDirection? SortDirection { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
