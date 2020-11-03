using Motel.Domain.Domain.Post;
using Motel.Services.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.RentalPosting.Caching
{
    public class CategoryCacheEventConsumer: CacheEventConsumer<Category>
    {
        protected override void ClearCache(Category entity)
        {
            RemoveByPrefix(MotelCatalogDefaults.CategoriesDisplayedOnHomepagePrefixCacheKey);
            RemoveByPrefix(MotelCatalogDefaults.CategoriesAllPrefixCacheKey);
        }
    }
}
