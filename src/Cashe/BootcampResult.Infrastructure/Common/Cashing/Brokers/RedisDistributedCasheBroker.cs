using System.Text;
using BootcampResult.Domain.Common.Cashing;
using BootcampResult.Infrastructure.Common.Settings;
using BootcampResult.Persistence.Cashing.Brokers;
using Force.DeepCloner;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BootcampResult.Infrastructure.Common.Cashing.Brokers;

public class RedisDistributedCasheBroker(IOptions<CasheSettings> casheSettings, IDistributedCache distributedCache) : ICasheBroker
{
    public readonly DistributedCacheEntryOptions _entryOptions = new DistributedCacheEntryOptions()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(casheSettings.Value.AbsoluteExpirationTimeInSeconds),
        SlidingExpiration = TimeSpan.FromSeconds(casheSettings.Value.SlidingExpirationTimeInSeconds),
    };

    public async ValueTask<T?> GetAsync<T>(string key)
    {
        var value = await distributedCache.GetAsync(key);

        return value is not null ? JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(value)) : default;
    }

    public  ValueTask<bool> TryGetAsync<T>(string key, out T? value)
    {
        var foundEntity = distributedCache.GetString(key);

        if (foundEntity is not null)
        {
            value = JsonConvert.DeserializeObject<T>(foundEntity);
            return ValueTask.FromResult(true);
        }

        value = default;
        return ValueTask.FromResult(false);
    }

    public async ValueTask<T?> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory, CasheEntryOptions? entryOptions = default)
    {
        var foundEntity = await distributedCache.GetStringAsync(key);
        if (foundEntity is not null)
            return JsonConvert.DeserializeObject<T>(foundEntity);

        var value = await valueFactory();
        await SetAsync(key, value, entryOptions);
        
        return value;
    }

    public async ValueTask SetAsync<T>(string key, T value, CasheEntryOptions? entryOptions = default)
    {
        await distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value),
            GetCasheEntryOptions(entryOptions));
    }

    public async ValueTask DeleteAsync<T>(string key)
    {
        await distributedCache.RemoveAsync(key);
    }

    public DistributedCacheEntryOptions GetCasheEntryOptions(CasheEntryOptions? entryOptions)
    {
        if (entryOptions == default || !entryOptions.AbsoluteExpirationRelativeToNow.HasValue && entryOptions.SlidingExpiration.HasValue)
            return _entryOptions;

        var currentEntryOptions = _entryOptions.DeepClone();

        currentEntryOptions.AbsoluteExpirationRelativeToNow = entryOptions.AbsoluteExpirationRelativeToNow;
        currentEntryOptions.SlidingExpiration = entryOptions.SlidingExpiration;

        return currentEntryOptions;
    }
}