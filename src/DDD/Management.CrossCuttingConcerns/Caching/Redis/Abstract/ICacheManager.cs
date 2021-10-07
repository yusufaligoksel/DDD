using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.CrossCuttingConcerns.Caching.Redis.Abstract
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        Task<T> Get<T>(string key, int cacheTime, Func<Task<T>> acquire);
        object Get(string key);
        void Add(string key, object data, int duration);
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
    }
}
