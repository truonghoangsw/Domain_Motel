using Motel.Domain.Domain.Logging;
using Motel.Services.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Logging.Caching
{
    public partial class ActivityLogTypeCacheEventConsumer : CacheEventConsumer<ActivityLogType>
    {
        /// <summary>
        /// Clear cache data
        /// </summary>
        /// <param name="entity">Entity</param>
        protected override void ClearCache(ActivityLogType entity)
        {
            Remove(MotelLoggingDefaults.ActivityTypeAllCacheKey);
        }
    }
}
