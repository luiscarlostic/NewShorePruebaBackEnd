namespace NewshoreAir.Domain.CacheService
{
    public interface ICacheService
    {
        bool TryGetValue(string key, out string value);

        bool TryAddValue(string key, string value); 
    }
}
