using Motel.Core.Enum;
using Motel.Domain;
using Motel.Domain.ContextDataBase;
using Motel.Domain.Domain.Sercurity;
using Motel.Services.Caching;
using Motel.Services.Caching.Extensions;
using Motel.Services.Events;
using Motel.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motel.Services.Security
{
    public class RolesUserServices : IRolesUserServices
    {
        #region Fields
        protected readonly IRepository<Auth_Roles>  _repositoryRoles;
        protected readonly IRepository<Auth_Assign>  _permissionAssign;
        protected readonly IRepository<Auth_UserRoles>  _repositoryRolesUser;
        protected readonly IEventPublisher _eventPublisher;
        protected readonly ICacheKeyService _cacheKeyService;
        protected readonly ILogger _logger;
        protected readonly IWorkContext _workContext;
       
        #endregion

        #region Ctor
         public RolesUserServices(IRepository<Auth_Roles> repositoryRoles, IRepository<Auth_Assign> permissionAssign, 
            IEventPublisher eventPublisher, ICacheKeyService cacheKeyService, ILogger logger,IWorkContext workContext,IRepository<Auth_UserRoles> repositoryRolesUser)
        {
            _repositoryRoles = repositoryRoles;
            _permissionAssign = permissionAssign;
            _eventPublisher = eventPublisher;
            _cacheKeyService = cacheKeyService;
            _repositoryRolesUser = repositoryRolesUser;
            _logger = logger;
            _workContext = workContext;
        }

        #endregion

        #region Methods
        public void DeleteRoles(Auth_Roles roles)
        {
            if(roles == null)
                throw new ArgumentNullException(nameof(roles));
            var query = _repositoryRolesUser.Table.Where(x=>x.RoleID == roles.Id);
            foreach (var item in query.ToList())
            {
                _repositoryRolesUser.Delete(item);
                _eventPublisher.EntityDeleted(item);
            }
            _repositoryRoles.Delete(roles);
            _eventPublisher.EntityDeleted(roles);
        }

        public IList<Auth_Assign> GetPermissonOfRole(int roleId)
        {
             var key = _cacheKeyService.PrepareKeyForDefaultCache(MotelSecurityDefaults.PermissionsAllowedCacheKey, ObjectTypeEnum.User,roleId);

             var query = from pa in _permissionAssign.Table
                where pa.ObjectID == roleId && pa.ObjectType == (int)ObjectTypeEnum.Role
                select pa;

            return query.ToCachedList(key);
        }

        public IList<Auth_Assign> GetPermissonOfRole(Auth_Roles role)
        {
            if(role == null)
                throw new ArgumentNullException(nameof(role));
            return GetPermissonOfRole(role.Id);
        }

        public Auth_Roles GetRoleById(int Id)
        {
            return _repositoryRoles.GetById(Id);
        }

        public IList<Auth_Roles> GetRoles(int pageIndex = 0, int pageSize = int.MaxValue, string Name = "")
        {
            var query = _repositoryRoles.Table;
            if(string.IsNullOrEmpty(Name))
                query = query.Where(x=>x.Name.Contains(Name));
            query = query.Skip(pageSize * pageIndex).Take(pageSize);
            return query.ToList();
        }

        public void InsertRoles(Auth_Roles roles)
        {
            if(roles == null)
                throw new ArgumentNullException(nameof(roles));

              _repositoryRoles.Insert(roles);

            _eventPublisher.EntityInserted(roles);
        }

        public void UpdateRoles(Auth_Roles roles)
        {
             if(roles == null)
                throw new ArgumentNullException(nameof(roles));
             _repositoryRoles.Update(roles);

            _eventPublisher.EntityUpdated(roles);
        }
        #endregion

    }
}
