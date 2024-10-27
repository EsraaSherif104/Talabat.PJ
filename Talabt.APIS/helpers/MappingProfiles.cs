using AutoMapper;
using Talabat.Core.Entities;
using Talabt.APIS.DTO;

namespace Talabt.APIS.helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDTO>()
                         .ForMember(d=>d.ProductType,o=>o.MapFrom(s=>s.ProductType.Name))
                         .ForMember(d=>d.ProductBrand,o=>o.MapFrom(s=>s.ProductBrand.Name));
        }
    }
}
