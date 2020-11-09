using Motel.Core.Caching;
using Motel.Domain.Domain.Auth;
using Motel.Services.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Security.Caching
{
    public class UserCacheEventConsumer: CacheEventConsumer<Auth_User>
    {
        protected override void ClearCache(Auth_User entity)
        {
            var keyRoles = _cacheKeyService.PrepareKeyForShortTermCache(MotelUserServicesDefaults.UserRolesByObjectCacheKey, entity);
            Remove(keyRoles);
            var keyID = _cacheKeyService.PrepareKeyForShortTermCache(MotelUserServicesDefaults.UserRoleIdsCacheKey, entity);
            Remove(keyID);
            
        }
    }
}
