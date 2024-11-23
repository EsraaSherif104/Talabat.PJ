using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Core_Aggra;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        //seeding
        public static async Task SeedAsync(StoreContext dbcontext)
        {
            if (!dbcontext.ProductBrands.Any())
            {
                var BrandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        await dbcontext.Set<ProductBrand>().AddAsync(Brand);
                    }
                    await dbcontext.SaveChangesAsync();

                }
            }
            //seeding type
            if (!dbcontext.ProductTypes.Any())
            {
                var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        await dbcontext.Set<ProductType>().AddAsync(Type);

                    }
                    await dbcontext.SaveChangesAsync();

                }
            }
            //seeding product
            if (!dbcontext.Products.Any())
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    
                        await dbcontext.Set<Product>().AddAsync(product);

                    

                }

            }
           
            if (!dbcontext.DeliveryMethod.Any())
            {
                var DeliveryMethodData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);
                if (DeliveryMethods?.Count > 0)
                {
                    foreach (var DeliveryMethod in DeliveryMethods)

                        await dbcontext.Set<DeliveryMethod>().AddAsync(DeliveryMethod);



                }

            }



            await dbcontext.SaveChangesAsync();

        }
    }
}
