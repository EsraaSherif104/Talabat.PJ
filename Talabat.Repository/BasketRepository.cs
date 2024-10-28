using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.Repository
{
    internal class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database=redis.GetDatabase();
        }





        public async Task<bool> DeleteBasketAsync(String BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }

       

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var basket =await _database.StringGetAsync(BasketId);
            //if (basket.IsNull) return null;
            //else 
            //    var returedBasked=JsonSerializer.Deserialize<CustomerBasket?>(basket);
          return basket.IsNull?null:JsonSerializer.Deserialize<CustomerBasket?>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
            var jsonBasket = JsonSerializer.Serialize(Basket);
          
          var createdOrUpdated=  await _database.StringSetAsync(Basket.Id, jsonBasket,TimeSpan.FromDays(1));

            if (!createdOrUpdated) return null;

            return await GetBasketAsync(Basket.Id);
        }
    }
}
