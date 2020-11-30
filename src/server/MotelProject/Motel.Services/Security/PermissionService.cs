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
using Motel.Core.Enum;

namespace Motel.Services.Security
{
    public class PermissionService: IPermissionService
    {
        
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IUserService _userService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<Auth_Permission> _permissionRecordRepository;
        private readonly IRepository<Auth_Assign> _permissionAssign;
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
            _permissionAssign = permissionRecordCustomerRoleMappingRepository;
            _staticCacheManager = staticCacheManager;
            _workContext = workContext;
            _logger = logger;
        }


        #endregion

        #region Utilities
        
        /// <summary>
        /// Get permission records by customer role identifier
        /// </summary>
        /// <param name="userRoleId">Customer role identifier</param>
        /// <returns>Permissions</returns>
        protected virtual IList<Auth_Assign> GetPermissionRecordsByUserRoleId(int userRoleId,int objectType)
        {
            var key = _cacheKeyService.PrepareKeyForDefaultCache(MotelSecurityDefaults.PermissionsAllowedCacheKey, objectType,userRoleId);

             var query = from pa in _permissionAssign.Table
                where pa.ObjectID == userRoleId && pa.ObjectType == objectType
                select pa;

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
            if(Authorize(auth_PermissionSystemName, customer.Id,(int)ObjectTypeEnum.User))
                return true;
            var userRoles = _userService.GetUserRoles(customer);
            foreach (var role in userRoles)
            if (Authorize(auth_PermissionSystemName, role.Id,(int)ObjectTypeEnum.Role))
                return true;
            return false;
        }

        public bool Authorize(string auth_PermissionSystemName, int userRoleId,int objectType)
        {
             if (string.IsNullOrEmpty(auth_PermissionSystemName))
                return false;

            var key = _cacheKeyService.PrepareKeyForDefaultCache(MotelSecurityDefaults.PermissionsAllowedCacheKey , objectType,userRoleId);
            var permissions = _staticCacheManager.Get(key, () =>GetPermissionRecordsByUserRoleId(userRoleId,objectType));
            foreach (var permission in permissions)
                    if (permission.Permission.ToLower().Trim() == auth_PermissionSystemName.ToLower().Trim())
                        return true;

                return false;
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

        public void DeleteAuth_PermissionUserMapping(int permissionId, int customerRoleId)
        {
            var permissionRecord = GetAuth_PermissionById(permissionId);
             var mapping = _permissionAssign.Table.FirstOrDefault(prcm => prcm.ObjectID == customerRoleId && prcm.ObjectType == (int)ObjectTypeEnum.User && prcm.Permission == permissionRecord.Permission);

            if (mapping is null)
                throw new Exception(string.Empty);

            _permissionAssign.Delete(mapping);

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
            var query = _permissionAssign.Table;
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

        public void InsertAuth_PermissionUserMapping(Auth_Assign auth_PermissionUserMapping)
        {
           if (auth_PermissionUserMapping is null)
                throw new ArgumentNullException(nameof(auth_PermissionUserMapping));
            auth_PermissionUserMapping.ObjectType = (int)ObjectTypeEnum.User;
            _permissionAssign.Insert(auth_PermissionUserMapping);

            //event notification
            _eventPublisher.EntityInserted(auth_PermissionUserMapping);
        }

        public void UpdateAuth_Permission(Auth_Permission permission)
        {
           
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRecordRepository.Update(permission);

            //event notification
            _eventPublisher.EntityUpdated(permission);
        }

        public void InsertAuth_PermissionRolesMapping(Auth_Assign auth_PermissionRoleMapping)
        {
           if (auth_PermissionRoleMapping is null)
                throw new ArgumentNullException(nameof(auth_PermissionRoleMapping));
            auth_PermissionRoleMapping.ObjectType = (int)ObjectTypeEnum.Role;
            _permissionAssign.Insert(auth_PermissionRoleMapping);

            //event notification
            _eventPublisher.EntityInserted(auth_PermissionRoleMapping);
        }

        #endregion
    }
}
