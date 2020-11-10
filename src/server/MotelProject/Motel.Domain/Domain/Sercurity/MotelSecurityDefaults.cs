using Motel.Core.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Sercurity
{
    public class MotelSecurityDefaults
    {
          /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} :permission name
        /// {1} :  object type 
        /// {2}:user ID or role Id
        /// </remarks>
        public static CacheKey PermissionsAllowedCacheKey => new CacheKey("Motel.permission.allowed-{0}-{1}", PermissionsAllowedPrefixCacheKey);
        public static string PermissionsAllowedPrefixCacheKey => "Motel.permission.allowed-{0}";

        public static string PermissionsAllByUserRoleIdPrefixCacheKey => "Motel.permission.allbyuserid";
        public static CacheKey PermissionsAllByUserRoleIdCacheKey => new CacheKey("Motel.permission.allbyuserid-{0}-{1}", PermissionsAllByUserRoleIdPrefixCacheKey);
        
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : Role system name
        /// {1} : user ID
        /// </remarks>
        public static CacheKey RolesAllowedCacheKey => new CacheKey("Motel.roles.allowed-{0}", RolesAllowedPrefixCacheKey);
        public static string RolesAllowedPrefixCacheKey => "Motel.roles.allowed";

      
        public static string RolesAllByUserIdPrefixCacheKey => "Motel.roles.allbyuserid";

         /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : user role ID
        /// </remarks>
        public static CacheKey UserAllByUserRoleIdCacheKey => new CacheKey("Motel.roles.allbyuserid-{0}", PermissionsAllByUserRoleIdPrefixCacheKey);
        
    }
}
