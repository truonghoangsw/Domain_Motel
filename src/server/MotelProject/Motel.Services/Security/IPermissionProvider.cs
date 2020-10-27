using Motel.Domain.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Security
{
    public interface IPermissionProvider
    {
        /// <summary>
        /// Get permissions
        /// </summary>
        /// <returns>Permissions</returns>
        IEnumerable<Auth_Permission> GetPermissions();

        /// <summary>
        /// Get default permissions
        /// </summary>
        /// <returns>Default permissions</returns>
        HashSet<(string systemRoleName, Auth_Permission[] permissions)> GetDefaultPermissions();
    }
}
