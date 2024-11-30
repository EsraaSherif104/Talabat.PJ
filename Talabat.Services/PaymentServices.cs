using Microsoft.Extensions.Configuration;
using Stripe;
using System;
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
using Talabat.Core.Specifications.OrderSpecifications;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Services
{
    public class PaymentServices : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUniteOfWork _uniteOfWork;

        public PaymentServices(IConfiguration configuration,
            IBasketRepository basketRepository,
            IUniteOfWork uniteOfWork)
        {
            this._configuration = configuration;
            this._basketRepository = basketRepository;
            this._uniteOfWork = uniteOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSetting:SecretKey"];

            var basket = await _basketRepository.GetBasketAsync(BasketId);
            if (basket is null) return null;
            var shippingPrice = 0M;
            //ann=subtotal+dm.cost
            if (basket.deliveryMethodId.HasValue)
            {
                var deliveryMethod=await _uniteOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.deliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;
            
            }
            if(basket.Item.Count>0)
            {
                foreach (var item in basket.Item)
                {
                    var product =await _uniteOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    if(item.Price!=product.Price)
                        item.Price= product.Price;
                
                }
            }
            var subTotal = basket.Item.Sum(i => i.Price + i.Quantity);
            var services = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)subTotal * 100 + (long)shippingPrice
                    , Currency = "usd",
                    PaymentMethodTypes=new List<string>() { "card"}
                };

                paymentIntent= await services.CreateAsync(options);
               basket.PaymentIntentId= paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            
            }
            else //update
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)subTotal * 100 + (long)(shippingPrice) * 100
                };

             paymentIntent= await  services.UpdateAsync(basket.PaymentIntentId, options);

                basket.PaymentIntentId=paymentIntent.Id;
                basket.ClientSecret=paymentIntent.ClientSecret;
            }
          await  _basketRepository.UpdateBasketAsync(basket);
        
          return basket;
        }

        public async Task<Order> UpdatePaymentIntentToSuccedOrFailed(string PaymentINtentid, bool flag)
        {
            var spec = new OrderWithPaymentIntentSpec(PaymentINtentid);
            var order = await _uniteOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);
            if (flag)
            {
                order.Status = OrderStatus.PaymentReceived;
            }
            else
            {
                order.Status = OrderStatus.PaymentFailed;
            }
            _uniteOfWork.Repository<Order>().Update(order);
          await  _uniteOfWork.CompleteAsync();
            return order;
        }
    }
}
