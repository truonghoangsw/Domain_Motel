using Motel.Core.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Sercurity
{
    public class MotelSecurityDefaults
    {
        public static CacheKey PermissionsAllowedCacheKey => new CacheKey("Motel.permission.allowed-{0}-{1}", PermissionsAllowedPrefixCacheKey);
        public static string PermissionsAllByCustomerRoleIdPrefixCacheKey => "Motel.permission.allbycustomerroleid";

        public static string PermissionsAllowedPrefixCacheKey => "Motel.permission.allowed-{0}";

    }
}
