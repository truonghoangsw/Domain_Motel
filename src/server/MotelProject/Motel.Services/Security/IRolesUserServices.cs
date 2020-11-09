using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Sercurity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Security
{
    public interface IRolesUserServices
    {
        /// <summary>
        /// Get roles
        /// </summary>
        /// <returns>Permissions</returns>
        IList<Auth_Roles> GetRoles(int pageIndex = 0,int pageSize = int.MaxValue,string Name ="");
        Auth_Roles GetRoleById(int Id);
        IList<Auth_Assign> GetPermissonOfRole(int roleId);
        IList<Auth_Assign> GetPermissonOfRole(Auth_Roles role);
        void InsertRoles(Auth_Roles roles);
        void UpdateRoles(Auth_Roles roles);
        void DeleteRoles(Auth_Roles roles);
    }
}
