using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Web.Pagination
{
    public class FilterRequestDto
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public FilterOperation? Operation { get; set; }
    }
}
