using BootcampResult.Domain.Common.Cashing;
using BootcampResult.Infrastructure.Common.Settings;
using BootcampResult.Persistence.Cashing.Brokers;
using LazyCache;
using Force.DeepCloner;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;

public class LazyMemoryCasheBroker(IAppCache appCashe, IOptions<CasheSettings> casheSettings) : ICasheBroker
{
    private readonly MemoryCacheEntryOptions _entryOptions = new MemoryCacheEntryOptions()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(casheSettings.Value.AbsoluteExpirationTimeInSeconds),
        SlidingExpiration = TimeSpan.FromSeconds(casheSettings.Value.SlidingExpirationTimeInSeconds),
    };
    
    public async ValueTask<T?> GetAsync<T>(string key)
    {
        return await appCashe.GetAsync<T>(key);
    }

    public ValueTask<bool> TryGetAsync<T>(string key, out T? value)
    {
        return new(appCashe.TryGetValue(key, out value));
    }

    public async ValueTask<T?> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory, CasheEntryOptions? entryOptions = default)
    {
        return await appCashe.GetOrAddAsync(key, valueFactory, GetCasheEntryOptions(entryOptions));
    }

    public ValueTask SetAsync<T>(string key, T value, CasheEntryOptions? entryOptions = default)
    {
        appCashe.Add(key, value, new MemoryCacheEntryOptions());
        
        return ValueTask.CompletedTask;
    }

    public ValueTask DeleteAsync<T>(string key)
    {
        appCashe.Remove(key);
        return ValueTask.CompletedTask;
    }

    public MemoryCacheEntryOptions GetCasheEntryOptions(CasheEntryOptions? entryOptions)
    {
        if (entryOptions is null || (!entryOptions.AbsoluteExpirationRelativeToNow.HasValue &&
                                          !entryOptions.SlidingExpiration.HasValue))
            return _entryOptions;

        var currentEntryOption = _entryOptions.DeepClone();

        currentEntryOption.AbsoluteExpirationRelativeToNow = entryOptions.AbsoluteExpirationRelativeToNow;
        currentEntryOption.SlidingExpiration = entryOptions.SlidingExpiration;

        return currentEntryOption;
    }
}