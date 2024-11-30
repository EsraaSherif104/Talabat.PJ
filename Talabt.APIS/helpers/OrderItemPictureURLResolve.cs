using AutoMapper;
using Talabat.Core.Entities.Core_Aggra;
using Talabt.APIS.DTO;

namespace Talabt.APIS.helpers
{
    public class OrderItemPictureURLResolve : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureURLResolve(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.Product.ProductUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.Product.ProductUrl}";
            }
            return string.Empty;
        }
    }
}
