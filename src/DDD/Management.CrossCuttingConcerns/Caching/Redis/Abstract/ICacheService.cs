using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.CrossCuttingConcerns.Caching.Redis.Abstract
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task<object> GetAsync(string key);
        Task<bool> AddOrUpdateAsync(string key, object data, TimeSpan? duration = null);
        Task<bool> RemoveAsync(string key);
        void RemoveByPattern(string pattern); //"Error.*"
        void RemoveAllKeysByServer();
        void RemoveAllKeys();
    }
}
