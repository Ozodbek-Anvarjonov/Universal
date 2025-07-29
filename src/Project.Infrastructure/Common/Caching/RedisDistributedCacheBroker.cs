using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Persistence.Caching.Brokers;
using Project.Persistence.Caching.Models;
using Project.Persistence.Caching.Settings;
using System.Text;

namespace Project.Infrastructure.Common.Caching;

public class RedisDistributedCacheBroker : ICacheBroker
{
    private readonly DistributedCacheEntryOptions defaultCacheEntryOptions;
    private readonly IDistributedCache distributedCache;

    public RedisDistributedCacheBroker(IOptions<CacheSettings> options, IDistributedCache distributedCache)
    {
        this.distributedCache = distributedCache;
        defaultCacheEntryOptions = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(options.Value.AbsoluteExpirationInMinutes),
            SlidingExpiration = TimeSpan.FromMinutes(options.Value.SlidingExpirationInMinutes),
        };
    }

    public async ValueTask<TEntity?> GetAsync<TEntity>(string key, CancellationToken cancellationToken = default)
    {
        var value = await distributedCache.GetStringAsync(key, cancellationToken);

        return value is not null ? JsonConvert.DeserializeObject<TEntity>(value) : default;
    }

    public ValueTask<bool> TryGetAsync<TEntity>(string key, out TEntity? value, CancellationToken cancellationToken = default)
    {
        var foundEntity = distributedCache.GetString(key);

        if (foundEntity is not null)
        {
            value = JsonConvert.DeserializeObject<TEntity>(foundEntity);
            return ValueTask.FromResult(true);
        }

        value = default;
        return ValueTask.FromResult(false);
    }

    public async ValueTask<TEntity?> GetOrSetAsync<TEntity>(
        string key,
        Func<Task<TEntity>> valueFactory,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
        )
    {
        var foundEntity = await distributedCache.GetStringAsync(key, cancellationToken);
        if (foundEntity is not null)
            return JsonConvert.DeserializeObject<TEntity>(foundEntity);

        var value = await valueFactory();
        await SetAsync(key, value, entryOptions, cancellationToken);

        return value;
    }

    public async ValueTask SetAsync<TEntity>(
        string key,
        TEntity value,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
        )
    {
        await distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value),
            GetCacheEntryOptions(entryOptions), cancellationToken);
    }

    public async ValueTask DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        await distributedCache.RemoveAsync(key, cancellationToken);
    }

    private DistributedCacheEntryOptions GetCacheEntryOptions(CacheEntryOptions? entryOptions) =>
        entryOptions is null ? defaultCacheEntryOptions
            : new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = entryOptions.AbsoluteExpirationRelativeToNow
                    ?? defaultCacheEntryOptions.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = entryOptions.SlidingExpiration
                    ?? defaultCacheEntryOptions.SlidingExpiration
            };
}