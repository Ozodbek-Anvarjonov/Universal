using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Project.Persistence.Caching.Brokers;
using Project.Persistence.Caching.Models;
using Project.Persistence.Caching.Settings;

namespace Project.Infrastructure.Common.Caching;

public class MemoryCacheBroker : ICacheBroker
{
    private readonly MemoryCacheEntryOptions defaultCacheEntryOptions;
    private readonly IMemoryCache memoryCache;

    public MemoryCacheBroker(IOptions<CacheSettings> options, IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
        defaultCacheEntryOptions = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(options.Value.AbsoluteExpirationInMinutes),
            SlidingExpiration = TimeSpan.FromMinutes(options.Value.SlidingExpirationInMinutes),
        };
    }

    public ValueTask<TEntity?> GetAsync<TEntity>(string key, CancellationToken cancellationToken = default)
    {
        memoryCache.TryGetValue<TEntity>(key, out var value);
        return new ValueTask<TEntity?>(value);
    }

    public ValueTask<bool> TryGetAsync<TEntity>(
        string key,
        out TEntity? entity,
        CancellationToken cancellationToken = default
        )
    {
        if (memoryCache.TryGetValue<TEntity>(key, out var value))
        {
            entity = value;
            return ValueTask.FromResult(true);
        }

        entity = default;
        return ValueTask.FromResult(false);
    }

    public async ValueTask<TEntity?> GetOrSetAsync<TEntity>(
        string key,
        Func<Task<TEntity>> valueFactory,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
        )
    {
        if (memoryCache.TryGetValue<TEntity>(key, out var value)) return value;

        value = await valueFactory();
        await SetAsync(key, value, entryOptions, cancellationToken);

        return value;
    }

    public ValueTask SetAsync<TEntity>(
        string key,
        TEntity entity,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
        )
    {
        memoryCache.Set(key, entity, GetCacheEntryOptions(entryOptions));
        return ValueTask.CompletedTask;
    }

    public ValueTask DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        memoryCache.Remove(key);
        return ValueTask.CompletedTask;
    }

    private MemoryCacheEntryOptions GetCacheEntryOptions(CacheEntryOptions? entryOptions) =>
        entryOptions is null ? defaultCacheEntryOptions
            : new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = entryOptions.AbsoluteExpirationRelativeToNow
                    ?? defaultCacheEntryOptions.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = entryOptions.SlidingExpiration
                    ?? defaultCacheEntryOptions.SlidingExpiration
            };
}