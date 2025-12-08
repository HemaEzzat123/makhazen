using MAKHAZIN.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Sepecification
{
    public class ProductsWithFiltirationSpecification : BaseSepecification<Product>
    {
        public ProductsWithFiltirationSpecification(string? search,int skip,int take)
            : base(
                  x => string.IsNullOrEmpty(search) || x.Name.ToLower().Contains(search) || x.Description.ToLower().Contains(search)
                  )
        {
            AddOrderBy(p => p.Name);
            ApplyPagination(skip, take);
            Includes.Add(p => p.StockItems);
        }
    }
}
