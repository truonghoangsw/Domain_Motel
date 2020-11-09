using Motel.Domain.Domain.Sercurity;
using Motel.Services.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Security.Caching
{
    public class UserRoleMappingEventConsumer:CacheEventConsumer<Auth_UserRoles>
    {
        public UserRoleMappingEventConsumer(Auth_UserRoles auth_UserRole)
        {
            var keyIds = _cacheKeyService.PrepareKey(MotelUserServicesDefaults.UserRoleIdsCacheKey, auth_UserRole.UserID);
            var keyObjec = _cacheKeyService.PrepareKey(MotelUserServicesDefaults.UserRolesByObjectCacheKey, auth_UserRole.UserID);
            Remove(keyIds);
            Remove(keyObjec);
        }
    }
}
