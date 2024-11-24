using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Core_Aggra;
using Talabat.Core.Entities.identity;
using Talabt.APIS.DTO;

namespace Talabt.APIS.helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDTO>()
                         .ForMember(d=>d.ProductType,o=>o.MapFrom(s=>s.ProductType.Name))
                         .ForMember(d=>d.ProductBrand,o=>o.MapFrom(s=>s.ProductBrand.Name))
                         .ForMember(d=>d.PictureUrl,o=>o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Addres, AddressDTO>().ReverseMap();
            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<BasketItemDTO, BasketItem>();
            CreateMap<AddressDTO, Address>();


        }
    }
}
