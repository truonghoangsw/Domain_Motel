using Microsoft.AspNetCore.Http;
using Motel.Core.Configuration;
using Motel.Core.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Motel.Core.Caching
{
    public class RedisCacheManager : IStaticCacheManager
    {
        #region Fiels
        private bool _disposed;
        private readonly IDatabase _db;
        private readonly IRedisConnectionWrapper _connectionWrapper;
        private readonly MotelConfig _config;
        private readonly PerRequestCache _perRequestCache;
        #endregion
        public RedisCacheManager(IHttpContextAccessor httpContextAccessor,  IRedisConnectionWrapper connectionWrapper, MotelConfig config)
        {
            _perRequestCache = new PerRequestCache(httpContextAccessor);
            _connectionWrapper = connectionWrapper;
            _config = config;
            _db = connectionWrapper.GetDatabase(config.RedisDatabaseId ?? (int)RedisDatabaseNumber.Cache);
        }

        #region Method
             public void Clear()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(CacheKey key, Func<T> acquire)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(CacheKey key, Func<Task<T>> acquire)
        {
            throw new NotImplementedException();
        }

        public bool IsSet(CacheKey key)
        {
            throw new NotImplementedException();
        }

        public void Remove(CacheKey cachekey)
        {
            throw new NotImplementedException();
        }

        public void RemoveByPrefix(string prefix)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region PerRequestCache
        protected class PerRequestCache
        {
            #region Fiels
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ReaderWriterLockSlim _locker;
            #endregion

            #region Ctor
            public PerRequestCache(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;

                _locker = new ReaderWriterLockSlim();
            }
            #endregion

            #region Utilities
            protected virtual IDictionary<object, object> GetItems()
            {
                return _httpContextAccessor.HttpContext?.Items;
            }
            #endregion

        }
        #endregion
    }
}
