using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motel.Core.Caching
{
    public partial class CacheKey
    {
        #region Fields

        protected string _keyFormat = "";

        #endregion

         #region Ctor

        public CacheKey(CacheKey cacheKey, Func<object, object> createCacheKeyParameters, params object[] keyObjects)
        {
            Init(cacheKey.Key, cacheKey.CacheTime, cacheKey.Prefixes.ToArray());

            if(!keyObjects.Any())
                return;

            Key = string.Format(_keyFormat, keyObjects.Select(createCacheKeyParameters).ToArray());

            for (var i = 0; i < Prefixes.Count; i++)
                Prefixes[i] = string.Format(Prefixes[i], keyObjects.Select(createCacheKeyParameters).ToArray());
        }

        public CacheKey(string cacheKey, int? cacheTime = null, params string[] prefixes)
        {
            Init(cacheKey, cacheTime, prefixes);
        }

        public CacheKey(string cacheKey, params string[] prefixes)
        {
            Init(cacheKey, null, prefixes);
        }

        #endregion


        #region Utilities
        protected void Init(string cachekey,int? cachetime,string[] perfixes)
        {
            Key = cachekey;
            _keyFormat  = cachekey;
            if(cachetime.HasValue)
                CacheTime = cachetime.Value;

            Prefixes.AddRange(perfixes.Where(perfix => string.IsNullOrEmpty(perfix)));
        }
        #endregion

        public string Key { get; set; }
        public List<string> Prefixes { get; protected set; } = new List<string>();

        public int CacheTime { get; set; } =MotelCachingDefaults.CacheTime;
    }
}
