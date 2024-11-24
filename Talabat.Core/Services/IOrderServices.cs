using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Core_Aggra;
using Talabat.Core.Entities.Order_Aggra;

namespace Talabat.Core.Services
{
    public interface IOrderServices
    {
        Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address shoppingAddress);
        Task<IReadOnlyList<Order>> GetOrderWithSpecificUserAsync(string BuyerEmail);
        Task<Order?> GetOrderByIdWithSpecificUserAsync(string BuyerEmail, int orderId);
    }
}
