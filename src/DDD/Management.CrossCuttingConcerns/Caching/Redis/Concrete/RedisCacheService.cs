using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Management.CrossCuttingConcerns.Caching.Redis.Abstract;
using Management.Domain.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Management.CrossCuttingConcerns.Caching.Redis.Concrete
{
    public class RedisCacheService:ICacheService
    {
        #region Fields
        private readonly string _host;

        private readonly int _port;

        private readonly int _db;

        private ConnectionMultiplexer _ConnectionMultiplexer;
        private readonly RedisConnectionSetting _redisSettings;
        private readonly IDatabase _database;
        #endregion

        #region Ctor

        public RedisCacheService(IOptions<RedisConnectionSetting> redisSettings)
        {
            _redisSettings = redisSettings.Value;
            _host = _redisSettings.Host;
            _port = _redisSettings.Port;
            _db = _redisSettings.Database;
            Connect();
            _database = GetDb();
        }

        #endregion

        public void Connect() => _ConnectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

        public IDatabase GetDb() => _ConnectionMultiplexer.GetDatabase(_db);

        public async Task<T> GetAsync<T>(string key)
        {
            var result = await _database.StringGetAsync(key);

            if (String.IsNullOrWhiteSpace(result))
                return default(T);

            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<object> GetAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task<bool> AddOrUpdateAsync(string key, object data, TimeSpan? duration = null)
        {
            var result = await _database.StringSetAsync(key, JsonConvert.SerializeObject(data), duration);

            return result;
        }

        public Task<bool> RemoveAsync(string key)
        {
            var result = _database.KeyDeleteAsync(key);

            return result;
        }

        public void RemoveByPattern(string pattern)
        {
            var endpoints = _ConnectionMultiplexer.GetEndPoints();
            var server = _ConnectionMultiplexer.GetServer(endpoints.First());
            if (server != null)
            {
                foreach (var key in server.Keys(pattern: pattern)) //"Error.*"
                    _database.KeyDelete(key);
            }
        }

        public void RemoveAllKeysByServer()
        {
            var endpoints = _ConnectionMultiplexer.GetEndPoints();
            var server = _ConnectionMultiplexer.GetServer(endpoints.First());
            server.FlushDatabase(_db);
        }

        public void RemoveAllKeys()
        {
            var endpoints = _ConnectionMultiplexer.GetEndPoints();
            var server = _ConnectionMultiplexer.GetServer(endpoints.First());
            server.FlushAllDatabases();
        }
    }
}
