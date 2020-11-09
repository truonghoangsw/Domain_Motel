using Motel.Domain.Domain.Sercurity;
using Motel.Services.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Security.Caching
{
    public class UserPermissionMapEventConsumer:CacheEventConsumer<Auth_Assign>
    {
        protected override void ClearCache(Auth_Assign entity)
        {
            var key = _cacheKeyService.PrepareKeyForDefaultCache(MotelSecurityDefaults.PermissionsAllowedCacheKey,entity.ObjectType,entity.ObjectID);
            Remove(key);
            
        }
    }
}
