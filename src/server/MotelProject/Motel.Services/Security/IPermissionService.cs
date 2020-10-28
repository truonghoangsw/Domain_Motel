using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Sercurity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Security
{
     public partial interface IPermissionService
    {
        /// <summary>
        /// Delete a permission
        /// </summary>
        /// <param name="permission">Permission</param>
        void DeleteAuth_Permission(Auth_Permission permission);

        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="permissionId">Permission identifier</param>
        /// <returns>Permission</returns>
        Auth_Permission GetAuth_PermissionById(int permissionId);

        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="systemName">Permission system name</param>
        /// <returns>Permission</returns>
        Auth_Permission GetAuth_PermissionBySystemName(string systemName);

        /// <summary>
        /// Gets all permissions
        /// </summary>
        /// <returns>Permissions</returns>
        IList<Auth_Permission> GetAllAuth_Permissions();

        /// <summary>
        /// Inserts a permission
        /// </summary>
        /// <param name="permission">Permission</param>
        void InsertAuth_Permission(Auth_Permission permission);

        /// <summary>
        /// Updates the permission
        /// </summary>
        /// <param name="permission">Permission</param>
        void UpdateAuth_Permission(Auth_Permission permission);

      
        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(Auth_Permission permission);

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(Auth_Permission permission, Auth_User customer);

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="Auth_PermissionSystemName">Permission record system name</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(string Auth_PermissionSystemName);

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="Auth_PermissionSystemName">Permission record system name</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(string Auth_PermissionSystemName, Auth_User customer);

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="Auth_PermissionSystemName">Permission record system name</param>
        /// <param name="customerRoleId">Customer role identifier</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(string Auth_PermissionSystemName, int customerRoleId);

        /// <summary>
        /// Gets a permission record-customer role mapping
        /// </summary>
        /// <param name="permissionId">Permission identifier</param>
        IList<Auth_Assign> GetMappingByAuth_PermissionId(int permissionId);

        /// <summary>
        /// Delete a permission record-customer role mapping
        /// </summary>
        /// <param name="permissionId">Permission identifier</param>
        /// <param name="customerRoleId">Customer role identifier</param>
        void DeleteAuth_PermissionCustomerRoleMapping(int permissionId, int customerRoleId);

        /// <summary>
        /// Inserts a permission record-customer role mapping
        /// </summary>
        /// <param name="Auth_PermissionCustomerRoleMapping">Permission record-customer role mapping</param>
        void InsertAuth_PermissionCustomerRoleMapping(Auth_Assign Auth_PermissionCustomerRoleMapping);
    }
}
