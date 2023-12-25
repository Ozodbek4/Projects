using BootcampResult.Domain.Common.Cashing;

namespace BootcampResult.Persistence.Cashing.Brokers;

public interface ICasheBroker
{
    ValueTask<T?> GetAsync<T>(string key);

    ValueTask<bool> TryGetAsync<T>(string key, out T? value);

    ValueTask<T?> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory, CasheEntryOptions? entryOptions = default);

    ValueTask SetAsync<T>(string key, T value, CasheEntryOptions? entryOptions = default);

    ValueTask DeleteAsync<T>(string key); 
}