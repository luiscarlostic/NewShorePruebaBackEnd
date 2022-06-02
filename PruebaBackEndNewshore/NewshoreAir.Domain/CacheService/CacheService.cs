using NewshoreAir.Domain.Models;
using System;

namespace NewshoreAir.Domain.CacheService
{
    public class CacheService : ICacheService
    {
        public bool TryAddValue(string key, string value)
        {
            return Cache.AppCache.TryAdd(key, value);
        }

        public bool TryGetValue(string key, out string value)
        {
            return Cache.AppCache.TryGetValue(key, out value);
        }
    }
}
