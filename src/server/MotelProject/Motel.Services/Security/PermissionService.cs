using Motel.Core.Caching;
using Motel.Domain;
using Motel.Domain.ContextDataBase;
using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Sercurity;
using Motel.Services.Caching;
using System.Linq;
using System.Collections.Generic;
using Motel.Services.Caching.Extensions;
using System;
using Motel.Services.Events;
using Motel.Services.Logging;

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
        private readonly ILogger _logger;

        #region Ctor

        public PermissionService(ICacheKeyService cacheKeyService,
            IUserService userService,
            IEventPublisher eventPublisher,
            IRepository<Auth_Permission> permissionRecordRepository,
            IRepository<Auth_Assign> permissionRecordCustomerRoleMappingRepository,
            IStaticCacheManager staticCacheManager,
            IWorkContext workContext,ILogger logger)
        {
            _cacheKeyService = cacheKeyService;
            _userService = userService;
            _eventPublisher = eventPublisher;
            _permissionRecordRepository = permissionRecordRepository;
            _permissionRecordCustomerRoleMappingRepository = permissionRecordCustomerRoleMappingRepository;
            _staticCacheManager = staticCacheManager;
            _workContext = workContext;
            _logger = logger;
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
             return Authorize(permission, _workContext.CurrentUser);
        }

        public bool Authorize(Auth_Permission permission, Auth_User customer)
        {
            if (permission == null)
                return false;

            if (customer == null)
                return false;
             return Authorize(permission.Permission, customer);
        }

        public bool Authorize(string Auth_PermissionSystemName)
        {
            return Authorize(Auth_PermissionSystemName, _workContext.CurrentUser);
        }

        public bool Authorize(string auth_PermissionSystemName, Auth_User customer)
        {
            if (string.IsNullOrEmpty(auth_PermissionSystemName))
                return false;

            var userRoles = _userService.GetCustomerRoles(customer);
             foreach (var role in userRoles)
                if (Authorize(auth_PermissionSystemName, role.Id))
                    return true;
                else if(Authorize(auth_PermissionSystemName, customer.Id))
                    return true;
            return false;
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
               if (permission == null)
                    return;
                _permissionRecordRepository.Delete(permission);
                _eventPublisher.EntityDeleted(permission);
            }
            catch (System.Exception ex)
            {

                _logger.Error("DeleteAuth_Permission error",ex);
            }
        }

        public void DeleteAuth_PermissionCustomerRoleMapping(int permissionId, int customerRoleId)
        {
            var permissionRecord = GetAuth_PermissionById(permissionId);
             var mapping = _permissionRecordCustomerRoleMappingRepository.Table.FirstOrDefault(prcm => prcm.ObjectID == customerRoleId && prcm.Permission == permissionRecord.Permission);

            if (mapping is null)
                throw new Exception(string.Empty);

            _permissionRecordCustomerRoleMappingRepository.Delete(mapping);

            //event notification
            _eventPublisher.EntityDeleted(mapping);
        }

        public IList<Auth_Permission> GetAllAuth_Permissions()
        {
              var query = from pr in _permissionRecordRepository.Table
                        orderby pr.Name
                        select pr;
            var permissions = query.ToList();
            return permissions;
        }

        public Auth_Permission GetAuth_PermissionById(int permissionId)
        {
            if (permissionId == 0)
                return null;

            return _permissionRecordRepository.ToCachedGetById(permissionId);
        }

        public Auth_Permission GetAuth_PermissionBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

               var query = from pr in _permissionRecordRepository.Table
                        where pr.Permission == systemName
                        orderby pr.Id
                        select pr;

            var permissionRecord = query.FirstOrDefault();
            return permissionRecord;
        }

        public IList<Auth_Assign> GetMappingByAuth_PermissionId(int permissionId)
        {
            var query = _permissionRecordCustomerRoleMappingRepository.Table;
            var permission = GetAuth_PermissionById(permissionId);
            if(permission == null)
                return null;
            query = query.Where(x => x.Permission == permission.Permission);
            return query.ToList();
        }

        public void InsertAuth_Permission(Auth_Permission permission)
        {
              if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRecordRepository.Insert(permission);

            //event notification
            _eventPublisher.EntityInserted(permission);
        }

        public void InsertAuth_PermissionCustomerRoleMapping(Auth_Assign Auth_PermissionCustomerRoleMapping)
        {
           if (Auth_PermissionCustomerRoleMapping is null)
                throw new ArgumentNullException(nameof(Auth_PermissionCustomerRoleMapping));

            _permissionRecordCustomerRoleMappingRepository.Insert(Auth_PermissionCustomerRoleMapping);

            //event notification
            _eventPublisher.EntityInserted(Auth_PermissionCustomerRoleMapping);
        }

        public void UpdateAuth_Permission(Auth_Permission permission)
        {
           
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRecordRepository.Update(permission);

            //event notification
            _eventPublisher.EntityUpdated(permission);
        }
        #endregion 
    }
}
