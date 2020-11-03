using Motel.Core.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Territories
{
    public class TerritoriesDefaults
    {
        public static CacheKey TerritoriesAllCacheKey => new CacheKey("Motel.Territories.all", TerritoriesAllPrefixCacheKey);
        public static string TerritoriesAllPrefixCacheKey => "Motel.Territories.all";
        public static CacheKey TerritoriesParnetCacheKey => new CacheKey("Motel.Territories.Parnet-{0}-{1}", TerritoriesAllPrefixCacheKey);
        public static string TerritoriesParnetPrefixCacheKey => "Motel.Territories.Parnet";
    }
}
