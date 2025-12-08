using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Application.Features.Products.Query
{
    public class GetProductsQuery : IQuery<Pagination<ProductDTO>>
    {
        public string? Search { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
