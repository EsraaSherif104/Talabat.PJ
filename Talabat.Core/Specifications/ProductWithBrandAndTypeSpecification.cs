using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification:BaseSpecification<Product>
    {
        //is used for get all 
        public ProductWithBrandAndTypeSpecification():base()
        {
            Include.Add(p => p.ProductType);
            Include.Add(p => p.ProductBrand);
                }
    }
}
