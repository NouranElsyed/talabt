using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.Core.ServicesContext
{
    public interface ICacheService
    {
        Task SetCacheKeyAsync(string key, object response, TimeSpan expiretime);
        Task<string> GetCacheKeyAsync(string key);
    }
}
