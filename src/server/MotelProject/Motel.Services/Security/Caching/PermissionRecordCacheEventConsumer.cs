using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Sercurity;
using Motel.Services.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Security.Caching
{
    public class PermissionRecordCacheEventUser:CacheEventConsumer<Auth_Permission>
    {
        protected override void ClearCache(Auth_Permission entity)
        {
            var prefix = _cacheKeyService.PrepareKeyPrefix(MotelSecurityDefaults.PermissionsAllowedPrefixCacheKey, entity.Name);
            RemoveByPrefix(prefix);
            RemoveByPrefix(MotelSecurityDefaults.PermissionsAllByCustomerRoleIdPrefixCacheKey);
        }
    }
}
