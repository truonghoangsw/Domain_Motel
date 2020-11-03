using Motel.Core.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.RentalPosting
{
    public class MotelCatalogDefaults
    {
        public static CacheKey CategoriesAllCacheKey => new CacheKey("Motel.category.all-{0}", CategoriesAllPrefixCacheKey);
        public static string CategoriesAllPrefixCacheKey => "Motel.category.all";
        public static CacheKey CategoriesAllDisplayedOnHomepageCacheKey => new CacheKey("Motel.category.homepage.all", CategoriesDisplayedOnHomepagePrefixCacheKey);
        public static string CategoriesDisplayedOnHomepagePrefixCacheKey => "Motel.category.homepage";

    }
}
