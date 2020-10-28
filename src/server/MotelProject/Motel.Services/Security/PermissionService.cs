using Motel.Core.Caching;
using Motel.Domain;
using Motel.Domain.ContextDataBase;
using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Sercurity;
using Motel.Services.Caching;
using System.Linq
using Motel.Services.Events;
using System.Collections.Generic;
using Motel.Services.Caching.Extensions;
using System;

namespace Motel.Services.Security
{
    public class PermissionService: IPermissionService
    {
        
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IUserService _userService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<Auth_Permission> _permissionRecordRepository;
        private readonly IRepository<Auth_Assign> _permissionRecordCustomerRoleMappingRepository;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IWorkContext _workContext;

        #region Ctor

        public PermissionService(ICacheKeyService cacheKeyService,
            IUserService userService,
            IEventPublisher eventPublisher,
            IRepository<Auth_Permission> permissionRecordRepository,
            IRepository<Auth_Assign> permissionRecordCustomerRoleMappingRepository,
            IStaticCacheManager staticCacheManager,
            IWorkContext workContext)
        {
            _cacheKeyService = cacheKeyService;
            _userService = userService;
            _eventPublisher = eventPublisher;
            _permissionRecordRepository = permissionRecordRepository;
            _permissionRecordCustomerRoleMappingRepository = permissionRecordCustomerRoleMappingRepository;
            _staticCacheManager = staticCacheManager;
            _workContext = workContext;
        }


        #endregion

        #region Utilities
        
        /// <summary>
        /// Get permission records by customer role identifier
        /// </summary>
        /// <param name="customerRoleId">Customer role identifier</param>
        /// <returns>Permissions</returns>
        protected virtual IList<Auth_Permission> GetPermissionRecordsByCustomerRoleId(int customerRoleId)
        {
            var key = _cacheKeyService.PrepareKeyForDefaultCache(MotelSecurityDefaults.PermissionsAllByCustomerRoleIdCacheKey, customerRoleId);

             var query = from pr in _permissionRecordRepository.Table
                join prcrm in _permissionRecordCustomerRoleMappingRepository.Table on pr.Permission equals prcrm.
                     Permission
                orderby pr.Id
                select pr;

            return query.ToCachedList(key);
        }

        #endregion

        #region Method

        public bool Authorize(Auth_Permission permission)
        {
            throw new System.NotImplementedException();
        }

        public bool Authorize(Auth_Permission permission, Auth_User customer)
        {
            throw new System.NotImplementedException();
        }

        public bool Authorize(string Auth_PermissionSystemName)
        {
            throw new System.NotImplementedException();
        }

        public bool Authorize(string auth_PermissionSystemName, Auth_User customer)
        {
            if (string.IsNullOrEmpty(auth_PermissionSystemName))
                return false;

            var customerRoles = _userService.GetCustomerRoles(customer);

        }

        public bool Authorize(string auth_PermissionSystemName, int customerRoleId)
        {
             if (string.IsNullOrEmpty(auth_PermissionSystemName))
                return false;

            var key = _cacheKeyService.PrepareKeyForDefaultCache(MotelSecurityDefaults.PermissionsAllowedCacheKey, auth_PermissionSystemName, customerRoleId);
            
            return _staticCacheManager.Get(key, () =>
            {
                var permissions = GetPermissionRecordsByCustomerRoleId(customerRoleId);
                foreach (var permission in permissions)
                    if (permission.Permission.Equals(auth_PermissionSystemName, StringComparison.InvariantCultureIgnoreCase))
                        return true;

                return false;
            });
        }

        public void DeleteAuth_Permission(Auth_Permission permission)
        {
            try
            {

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public void DeleteAuth_PermissionCustomerRoleMapping(int permissionId, int customerRoleId)
        {
            throw new System.NotImplementedException();
        }

        public IList<Auth_Permission> GetAllAuth_Permissions()
        {
            throw new System.NotImplementedException();
        }

        public Auth_Permission GetAuth_PermissionById(int permissionId)
        {
            throw new System.NotImplementedException();
        }

        public Auth_Permission GetAuth_PermissionBySystemName(string systemName)
        {
            throw new System.NotImplementedException();
        }

        public IList<Auth_Assign> GetMappingByAuth_PermissionId(int permissionId)
        {
            throw new System.NotImplementedException();
        }

        public void InsertAuth_Permission(Auth_Permission permission)
        {
            throw new System.NotImplementedException();
        }

        public void InsertAuth_PermissionCustomerRoleMapping(Auth_Assign Auth_PermissionCustomerRoleMapping)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAuth_Permission(Auth_Permission permission)
        {
            throw new System.NotImplementedException();
        }
        #endregion 
    }
}
