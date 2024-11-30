using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithFiltrationForCountAsync:BaseSpecification<Product>
    {
        public ProductWithFiltrationForCountAsync(ProductSpecParam param)
            : base(p =>
              (string.IsNullOrEmpty(param.Search) || p.Name.ToLower().Contains(param.Search))
            &&
            (!param.BrandID.HasValue || p.ProductBrandId == param.BrandID)
            &&
            (!param.TypeId.HasValue || p.ProductTypeId == param.TypeId)
            )
        {
            
        }
    }
}
