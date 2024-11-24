using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.Services;

namespace Talabt.APIS.helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expiredTimeInSecond;

        public CachedAttribute(int ExpiredTimeInSecond )
        {
            _expiredTimeInSecond = ExpiredTimeInSecond;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

          var CacheService=  context.HttpContext.RequestServices.GetRequiredService<IResponseCasheService>();
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
          var Cacheresponse=  await  CacheService.GetCacheRespone(cacheKey);
        
            if(!string.IsNullOrEmpty(Cacheresponse))
            {
                var contentResult = new ContentResult()
                {Content=Cacheresponse,
                ContentType="application/json",
                StatusCode=200

                };
                context.Result = contentResult;
                return;
            }
          var ExecutedEndPointContext=  await  next.Invoke();
            if(ExecutedEndPointContext.Result is OkObjectResult Result)
            {
               await CacheService.CacheResponseAsync(cacheKey, Result.Value, TimeSpan.FromSeconds(_expiredTimeInSecond));

           
            }
        
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var KeyBuilder = new StringBuilder();
            KeyBuilder.Append(request.Path);
            foreach (var (key,value) in request.Query.OrderBy(x=>x.Key))
            {
                KeyBuilder.Append($"|{key}-{value}");
                
            }
            return KeyBuilder.ToString();
        }
    }
}
