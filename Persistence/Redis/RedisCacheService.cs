using System;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Persistence.Redis
{
    public class RedisCacheService : ICacheService
    {
        private RedisServer _redisServer;

        public RedisCacheService(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public bool CheckActive()
        {
            try
            {
                _redisServer.Database.Ping();
                return true;
            } catch (RedisConnectionException ex)
            {
                return false;
            }
        }

        public void Add(string key, object data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            _redisServer.Database.StringSet(key, jsonData);
        }

        public bool Any(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        public List<T>? Get<T>(string key)
        {
            if (Any(key) && CheckActive())
            {
                string? jsonData = _redisServer.Database.StringGetSetExpiry(key, TimeSpan.FromMinutes(10));
                return JsonConvert.DeserializeObject<List<T>>(jsonData);
            }

            return default;
        }

        public void Remove(string key)
        {
            _redisServer.Database.KeyDelete(key);
        }

        public void Clear()
        {
            _redisServer.FlushDatabase();
        }
    }
}

