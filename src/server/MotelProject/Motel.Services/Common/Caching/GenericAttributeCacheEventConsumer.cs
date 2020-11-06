using Motel.Domain.Domain.Common;
using Motel.Services.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Common.Caching
{
    public class GenericAttributeCacheEventConsumer: CacheEventConsumer<GenericAttribute>
    {
        protected override void ClearCache(GenericAttribute entity)
        {
            var cacheKey = _cacheKeyService.PrepareKey(MotelCommonDefaults.GenericAttributeCacheKey, entity.EntityId, entity.KeyGroup);
            Remove(cacheKey);
        }
    }
}
