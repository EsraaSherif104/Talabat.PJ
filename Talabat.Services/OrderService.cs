using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Core_Aggra;
using Talabat.Core.Entities.Order_Aggra;
using Talabat.Core.Repositories;
using Talabat.Core.Services;

namespace Talabat.Services
{
    public class OrderService : IOrderServices
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<DeliveryMethod> _deliveryMethod;
        private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(IBasketRepository basketRepository,
            IGenericRepository<Product> ProductRepo
            ,IGenericRepository<DeliveryMethod> deliveryMethod
            ,IGenericRepository<Order>OrderRepo)
        {
            this._basketRepository = basketRepository;
            _productRepo = ProductRepo;
            this._deliveryMethod = deliveryMethod;
            _orderRepo = OrderRepo;
        }

        public async Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address shippingAddress)
        {
            var Basket=await _basketRepository.GetBasketAsync(BasketId);
            
            var OrderItems=new List<OrderItem>();

            if(Basket?.Item.Count > 0)
            {
                foreach(var item in Basket.Item)
                {
                    var Product =await _productRepo.GetByIdAsync(item.Id);
                    var ProductItemOrdered = new ProductItemOrder(Product.Id, Product.Name, Product.PictureUrl);

                    var OrderItem = new OrderItem(ProductItemOrdered, item.Quantity, Product.Price);
                    OrderItems.Add(OrderItem);


                }

            }

            var subTotal = OrderItems.Sum(item => item.Price * item.Quantity);

            var DeliveryMethod =await _deliveryMethod.GetByIdAsync(DeliveryMethodId);

            var Order = new Order(BuyerEmail, shippingAddress, DeliveryMethod, OrderItems, subTotal);

            await _orderRepo.AddAsync(Order);


        
        }

        public Task<Order> GetOrderByIdWithSpecificUserAsync(string BuyerEmail, int orderId)
        {
        }

        public Task<IReadOnlyList<Order>> GetOrderWithSpecificUserAsync(string BuyerEmail)
        {
        }
    }
}
