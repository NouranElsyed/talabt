using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using talabat.Core.ServicesContext;

namespace talabtAPIs.Helper
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int expiredTimeInSecond;

        public CachedAttribute(int ExpiredTimeInSecond) 
        {
            expiredTimeInSecond = ExpiredTimeInSecond;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cachedKey = GenerateCachedKey(context.HttpContext.Request);
            var cachedData = await CacheService.GetCacheKeyAsync(cachedKey);
            if (!string.IsNullOrEmpty(cachedData)) 
            {
                var contentResult = new ContentResult()
                {
                    Content = cachedData,
                    ContentType = "application/json",
                    StatusCode = 200
                };
            context.Result= contentResult;
                return;
            }

            var ExecutedEndPointContect = await next.Invoke();
            if (ExecutedEndPointContect.Result is OkObjectResult result) 
            {
                await CacheService.SetCacheKeyAsync(cachedKey, result.Value, TimeSpan.FromSeconds(expiredTimeInSecond));
            
            }
        }
        private string GenerateCachedKey(HttpRequest request) 
        {
            var keyBuider = new StringBuilder();
            keyBuider.Append(request.Path);
            foreach (var (key, value) in request.Query.OrderBy(Q => Q.Key)) 
            {
                keyBuider.Append($"|{key}:{value}");
            }
            return keyBuider.ToString();

        }
    }
}
