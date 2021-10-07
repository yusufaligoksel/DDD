using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Management.CrossCuttingConcerns.Caching.Redis.Abstract;
using Management.Domain.Enums;
using Management.Domain.Settings;
using Microsoft.Extensions.Options;
using ServiceStack.Redis;

namespace Management.CrossCuttingConcerns.Caching.Redis.Concrete
{
    public class RedisCacheManager : IRedisCacheManager
    {
        private readonly RedisConnectionSetting _redisSettings;
        public RedisCacheManager(IOptions<RedisConnectionSetting> redisSettings)
        {
            _redisSettings = redisSettings.Value;
        }
        private readonly Dictionary<CacheIndex, RedisEndpoint> _endpoints = new();

        private void RedisInvoker(Action<RedisClient> redisAction, CacheIndex cacheIndex = CacheIndex.Default)
        {
            if (!_endpoints.ContainsKey(cacheIndex))
                _endpoints.Add(cacheIndex, new RedisEndpoint
                {
                    Host = _redisSettings.Host,
                    Port = _redisSettings.Port,
                    Db = (long)cacheIndex
                });

            using (var client = new RedisClient(_endpoints[cacheIndex]))
                redisAction.Invoke(client);
        }

        public T Get<T>(string key)
        {
            return Get<T>(key, CacheIndex.Default);
        }

        public T Get<T>(string key, CacheIndex cacheIndex)
        {
            var result = default(T);
            RedisInvoker(x => { result = x.Get<T>(key); }, cacheIndex);
            return result;
        }

        public async Task<T> Get<T>(string key, int cacheTime, Func<Task<T>> acquire)
        {
            return await Get(key, cacheTime, acquire, CacheIndex.Default);
        }

        public object Get(string key)
        {
            var result = default(object);
            RedisInvoker(x => { result = x.Get<object>(key); });
            return result;
        }

        public void Add(string key, object data, int duration)
        {
            Add(key, data, duration, CacheIndex.Default);
            //RedisInvoker(x => x.Add(key, data, TimeSpan.FromMinutes(duration)));
        }

        public void Add(string key, object data, int duration, CacheIndex cacheIndex)
        {
            RedisInvoker(x => x.Add(key, data, TimeSpan.FromMinutes(duration)), cacheIndex);
        }

        public bool IsAdd(string key)
        {
            return IsAdd(key, CacheIndex.Default);
        }

        public bool IsAdd(string key, CacheIndex cacheIndex)
        {
            var isAdded = false;
            RedisInvoker(x => isAdded = x.ContainsKey(key), cacheIndex);
            return isAdded;
        }

        public void Remove(string key)
        {
            RedisInvoker(x => x.Remove(key));
        }

        public void RemoveByPattern(string pattern)
        {
            RedisInvoker(x => x.RemoveByPattern(pattern));
        }

        public async Task<T> Get<T>(string key, int cacheTime, Func<Task<T>> acquire, CacheIndex cacheIndex)
        {
            //item already is in cache, so return it
            if (IsAdd(key, cacheIndex))
                return Get<T>(key, cacheIndex);

            //or create it using passed function
            var result = await acquire();

            if (result != null)
                //and set in cache (if cache time is defined)
                Add(key, result, cacheTime > 0 ? cacheTime : 60, cacheIndex);

            return result;
        }

        public T Get<T>(string key, int cacheTime, Func<T> acquire, CacheIndex cacheIndex)
        {
            //item already is in cache, so return it
            if (IsAdd(key, cacheIndex))
                return Get<T>(key, cacheIndex);

            //or create it using passed function
            var result = acquire();

            if (result != null)
                //and set in cache (if cache time is defined)
                Add(key, result, cacheTime > 0 ? cacheTime : 60, cacheIndex);

            return result;
        }

        public void Clear(CacheIndex cacheIndex = CacheIndex.Default)
        {
            RedisInvoker(x => x.FlushAll(), cacheIndex);
        }
    }
}
