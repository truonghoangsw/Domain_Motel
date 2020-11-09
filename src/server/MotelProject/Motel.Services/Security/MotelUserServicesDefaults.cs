using Motel.Core.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Security
{
    public class MotelUserServicesDefaults
    {
        public static string UserRolesPrefixCacheKey => "Motel.userrole.";
        public static CacheKey UserRolesByObjectCacheKey => new CacheKey("Motel.userrole.roleObj-{0}", UserRolesPrefixCacheKey);


        public static string UserRolesIdPrefixCacheKey => "Motel.user.userrole";
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : user Id identifier
        /// </remarks>
        public static CacheKey UserRoleIdsCacheKey => new CacheKey("Motel.user.userrole.ids-{0}", UserRolesIdPrefixCacheKey);

      
    }
}
