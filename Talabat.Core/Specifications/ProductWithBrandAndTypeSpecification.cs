﻿using System;
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
        public ProductWithBrandAndTypeSpecification(ProductSpecParam param)
            : base(p=>
            (!param.BrandID.HasValue || p.ProductBrandId==param.BrandID)
            &&
            (!param.TypeId.HasValue ||p.ProductTypeId==param.TypeId)
            )
        {
            Include.Add(p => p.ProductType);
            Include.Add(p => p.ProductBrand);

            if (!string.IsNullOrEmpty(param.sort))
            {
                switch(param.sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(p=>p.Price);
                        break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }
        }

        //ctor for get product by id
        public ProductWithBrandAndTypeSpecification(int id):base(p=>p.Id==id)
        {
            Include.Add(p => p.ProductType);
            Include.Add(p => p.ProductBrand);
        }
    }
}
