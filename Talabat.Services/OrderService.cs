﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
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
        private readonly IUniteOfWork _uniteOfWork;

        public OrderService(IBasketRepository basketRepository
            ,IUniteOfWork uniteOfWork)
           
        {
            this._basketRepository = basketRepository;
            this._uniteOfWork = uniteOfWork;
            
        }

        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address shippingAddress)
        {
            var Basket=await _basketRepository.GetBasketAsync(BasketId);
            
            var OrderItems=new List<OrderItem>();

            if(Basket?.Item.Count > 0)
            {
                foreach(var item in Basket.Item)
                {
                    var Product =await _uniteOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var ProductItemOrdered = new ProductItemOrder(Product.Id, Product.Name, Product.PictureUrl);

                    var OrderItem = new OrderItem(ProductItemOrdered, item.Quantity, Product.Price);
                    OrderItems.Add(OrderItem);


                }

            }

            var subTotal = OrderItems.Sum(item => item.Price * item.Quantity);

            var DeliveryMethod =await _uniteOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);

            var Order = new Order(BuyerEmail, shippingAddress, DeliveryMethod, OrderItems, subTotal);

            await _uniteOfWork.Repository<Order>().AddAsync(Order);
          var result=  await _uniteOfWork.CompleteAsync();
            if (result <= 0) return null;
            return Order;



        
        }

        public Task<Order> GetOrderByIdWithSpecificUserAsync(string BuyerEmail, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrderWithSpecificUserAsync(string BuyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}