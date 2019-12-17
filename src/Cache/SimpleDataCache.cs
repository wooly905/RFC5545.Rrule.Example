using System;
using Microsoft.Extensions.Caching.Memory;
using RruleTool.Abstractions;

namespace RruleTool.Cache
{
    // This class lives in DI. Singleton type.
    internal class SimpleDataCache : ISimpleDataCache
    {
        private readonly MemoryCache _cache;
        // save in memory for 10 minutes
        private readonly int _itemExpirationDuration = 10;
        // cache size is 128 items
        private readonly int _cacheSizeLimit = 128;

        public SimpleDataCache()
        {
            MemoryCacheOptions options = new MemoryCacheOptions
            {
                // set scan expieration identical to item expired duration
                ExpirationScanFrequency = TimeSpan.FromMinutes(_itemExpirationDuration),
                SizeLimit = _cacheSizeLimit
            };

            _cache = new MemoryCache(options);
        }

        public void Set(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key) || value == null)
            {
                return;
            }

            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddMinutes(_itemExpirationDuration);
            options.SetSize(1);
            _cache.Set(key, value, options);
        }

        public bool TryGet(string key, out object value)
        {
            value = null;

            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            if (_cache.TryGetValue(key, out object result))
            {
                value = result;
                return true;
            }

            return false;
        }
    }
}
