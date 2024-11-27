using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Services;

namespace Talabat.Services
{
    public class ResponseCacheServices : IResponseCasheService
    {
        private readonly IDatabase _database;
        public ResponseCacheServices(IConnectionMultiplexer Radis)
        {
            _database = Radis.GetDatabase();
        }


        public async Task CacheResponseAsync(string CacheKey, object Response, TimeSpan ExpireTime)
        {
            if (Response is null) return;
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serializedResponse=JsonSerializer.Serialize(Response, options);
           await _database.StringSetAsync(CacheKey, serializedResponse, ExpireTime);
        }

        public async Task<string?> GetCacheRespone(string CacheKey)
        {
        var cacheResponse   =await _database.StringGetAsync(CacheKey);
            if (cacheResponse.IsNullOrEmpty) return null;
            return cacheResponse;
        
        }
    }
}
