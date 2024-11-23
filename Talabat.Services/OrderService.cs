using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Core_Aggra;
using Talabat.Core.Entities.Order_Aggra;
using Talabat.Core.Services;

namespace Talabat.Services
{
    public class OrderService : IOrderServices
    {
        public Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address shoppingAddress)
        {
        }

        public Task<Order> GetOrderByIdWithSpecificUserAsync(string BuyerEmail, int orderId)
        {
        }

        public Task<IReadOnlyList<Order>> GetOrderWithSpecificUserAsync(string BuyerEmail)
        {
        }
    }
}
