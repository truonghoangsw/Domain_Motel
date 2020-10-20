using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Motel.Core.Caching
{
    public interface IStaticCacheManager
    {
        T Get<T>(CacheKey key, Func<T> acquire);
        Task<T> GetAsync<T>(CacheKey key,Func<Task<T>> acquire);
        void Remove(CacheKey cachekey);
        bool IsSet(CacheKey key);
        void RemoveByPrefix(string prefix);
        void Clear();
    }
}
