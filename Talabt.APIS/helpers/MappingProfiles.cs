using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Core_Aggra;
using Talabat.Core.Entities.identity;
using Talabat.Core.Entities.Order_Aggra;
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

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(D => D.DeliveryMethodCost, O => O.MapFrom(S => S.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(D => D.ProductId, O => O.MapFrom(S => S.Product.ProductId))
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.ProductUrl, O => O.MapFrom(S => S.Product.ProductUrl))
                .ForMember(d=>d.ProductUrl,o=>o.MapFrom<OrderItemPictureURLResolve>());


        }
    }  
}
