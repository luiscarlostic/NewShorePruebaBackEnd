using System.Collections.Concurrent;

namespace NewshoreAir.Domain.Models
{
    public static class Cache
    {
        //HttpContext.Current.Cache.Add("Flights", flights, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration, CacheItemPriority.High, null);

        private static ConcurrentDictionary<string, string> cache;

        private static object cacheLock = new object();
        public static ConcurrentDictionary<string, string> AppCache
        {
            get
            {
                lock (cacheLock)
                {
                    if (cache == null)
                    {
                        cache = new ConcurrentDictionary<string, string>();
                    }
                    return cache;
                }
            }
        }
    }
}
