using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Management.Domain.Enums;

namespace Management.CrossCuttingConcerns.Caching.Redis.Abstract
{
    public interface IRedisCacheManager
    {
        Task<T> Get<T>(string key, int cacheTime, Func<Task<T>> acquire, CacheIndex cacheIndex);
        T Get<T>(string key, int cacheTime, Func<T> acquire, CacheIndex cacheIndex);
        void Add(string key, object data, int duration, CacheIndex cacheIndex);
        bool IsAdd(string key, CacheIndex cacheIndex);
        T Get<T>(string key, CacheIndex cacheIndex);
        void Clear(CacheIndex cacheIndex = CacheIndex.Default);
    }
}
