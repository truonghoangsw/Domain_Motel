using Motel.Core;
using Motel.Core.Caching;
using Motel.Core.Enum;
using Motel.Domain.ContextDataBase;
using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Sercurity;
using Motel.Services.Caching;
using Motel.Services.Caching.Extensions;
using Motel.Services.Events;
using Motel.Services.Logging;
using Motel.Services.Sercurity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Motel.Services.Security
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly CachingSettings _cachingSettings;
        private readonly UserSettings _userSettings;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<Auth_User> _userRepository;
        private readonly IRolesUserServices _rolesServices;
        private readonly IRepository<Auth_UserRoles> _userRolesMappingRepository;
        private readonly IRepository<Auth_Assign> _permissionAssign;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IMotelDataProvider _dataProvider;
        private readonly ILogger _logger;
        #endregion

        #region Ctor
        public UserService(CachingSettings cachingSettings,
            UserSettings userSettings,
            ICacheKeyService cacheKeyService,
            IRepository<Auth_Assign> permissionAssign,
            IEventPublisher eventPublisher,
            IRolesUserServices rolesServices,
            IRepository<Auth_User> userRepository,
            IRepository<Auth_UserRoles> userRolesMappingRepository,
            IStaticCacheManager staticCacheManager,
            ILogger logger,IMotelDataProvider dataProvider)
        {
            _cachingSettings = cachingSettings;
            _userSettings = userSettings;
            _cacheKeyService = cacheKeyService;
            _eventPublisher = eventPublisher;
            _userRolesMappingRepository = userRolesMappingRepository;
            _userRepository = userRepository;
            _staticCacheManager = staticCacheManager;
            _logger = logger;
            _permissionAssign = permissionAssign;
            _dataProvider = dataProvider;
            _rolesServices =rolesServices;
        }

        public IPagedList<Auth_User> GetAllUser(DateTime? createdFromUtc = null, DateTime? createdToUtc = null,
            int[] customerRoleIds = null, string email = null, string username = null, string firstName = null, 
            string lastName = null, int dayOfBirth = 0, int monthOfBirth = 0,string phone = null, string zipPostalCode = null, string ipAddress = null, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            try
            {
                var query = _userRepository.Table;
                query = query.Where(c => c.Deleted != 1);
                if (createdFromUtc.HasValue)
                    query = query.Where(c => createdFromUtc.Value <= c.CreatedTime);
                if (createdToUtc.HasValue)
                    query = query.Where(c => createdToUtc.Value >= c.CreatedTime);
              
                if (customerRoleIds != null && customerRoleIds.Length > 0)
                {
                    query = query.Join(_userRolesMappingRepository.Table, x => x.Id, y => y.UserID,
                            (x, y) => new { Customer = x, Mapping = y })
                        .Where(z => customerRoleIds.Contains(z.Mapping.RoleID))
                        .Select(z => z.Customer)
                        .Distinct();
                    }
                if (!string.IsNullOrWhiteSpace(email))
                    query = query.Where(c => c.Email.Contains(email));
                if (!string.IsNullOrWhiteSpace(username))
                    query = query.Where(c => c.UserName.Contains(username));
                if (!string.IsNullOrWhiteSpace(phone))
                    query = query.Where(c => c.PhoneNumber.Contains(phone));
                   query = query.OrderByDescending(c => c.CreatedTime);

                var users = new PagedList<Auth_User>(query, pageIndex, pageSize, getOnlyTotalCount);

                return users;
            }
            catch (Exception ex)
            {
                _logger.Error("GetAllUser error",ex);
                return null;
            }
        }

        public Auth_User GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;
            try
            {
                var query = from c in _userRepository.Table
                            orderby c.Id
                            where c.UserName == username
                            select c;
                var customer = query.FirstOrDefault();
                return customer;
            }
            catch (Exception ex)
            {
                _logger.Error("GetUserByUsername error", ex);
                return null;
            }
        }

        public Auth_User GetUserByEmail(string email)
        {
             if (string.IsNullOrWhiteSpace(email))
                return null;
            try
            {
                var query = from c in _userRepository.Table
                        orderby c.Id
                        where c.Email == email
                        select c;
                 var customer = query.FirstOrDefault();
                return customer;
            }
            catch (Exception ex)
            {
                _logger.Error("GetUserByUsername error",ex);
                return null;
            }
        }
        #endregion

     

        public void UpdateCustomerPassword(UserPassword userPassword)
        {
            try
            {
                //prepare parameters
                var pUserId =  SqlParameterHelper.GetInt32Parameter("@_UserId", userPassword.UserId);
                var pPasswordHash=  SqlParameterHelper.GetStringParameter("@_PasswordHash", userPassword.PasswordHash);
                _dataProvider.ExecuteNonQuery("UpdatePasswordUser",pUserId,pPasswordHash);
                var userId = _userRepository.GetById(userPassword.UserId);
                _eventPublisher.EntityUpdated(userId);
            }
            catch (Exception ex)
            {
                _logger.Error("UpdateCustomerPassword error", ex);
            }
        }

        public Auth_User InsertUserLester(Auth_User user)
        {
            try
            {
                user.Deleted = 0;
                user.CreatedTime = DateTime.Now;
                user.Status = (int)EnumStatusUser.Approved;
                _userRepository.Insert(user);
                _eventPublisher.EntityInserted(user);
                var roles = _rolesServices.GetRoleByName(RoleDefault.RoleLester);
                var usersRole = new Auth_UserRoles()
                {
                    UserID =user.Id,
                    RoleID = roles.Id
                };
                AddUserRoleMapping(usersRole);
                return user;

            }
            catch (Exception ex)
            {
                _logger.Error("UpdateCustomerPassword error", ex);
                return null;

            }
        }
        public Auth_User InsertUserRetener(Auth_User user)
        {
            try
            {
                user.CreatedTime = DateTime.Now;
                user.Status = (int)EnumStatusUser.Approved;
                _userRepository.Insert(user);
                _eventPublisher.EntityInserted(user);
                var roles = _rolesServices.GetRoleByName(RoleDefault.RoleRetener);
                var usersRole = new Auth_UserRoles()
                {
                    UserID =user.Id,
                    RoleID = roles.Id
                };
                AddUserRoleMapping(usersRole);
                return user;

            }
            catch (Exception ex)
            {
                _logger.Error("UpdateCustomerPassword error", ex);
                return null;

            }
        }
        public Auth_User InsertUserAdmin(Auth_User user)
        {
            try
            {
                user.CreatedTime = DateTime.Now;
                user.Status = (int)EnumStatusUser.Approved;
                user.IsAdmin = true;
                _userRepository.Insert(user);
                _eventPublisher.EntityInserted(user);
                return user;

            }
            catch (Exception ex)
            {
                _logger.Error("UpdateCustomerPassword error", ex);
                return null;

            }
        }

        public void UpdateUser(Auth_User customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            _userRepository.Update(customer);

            //event notification
            _eventPublisher.EntityUpdated(customer);
        }


        #region User roles
        public IList<Auth_UserRoles> GetUserRoles(Auth_User user, bool showHidden = false)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            try
            {
                var key = _cacheKeyService.PrepareKeyForShortTermCache(MotelUserServicesDefaults.UserRolesByObjectCacheKey, user);

                var query = from ur in  _userRolesMappingRepository.Table join urm in _userRolesMappingRepository.Table
                            on ur.Id equals  urm.RoleID
                            where urm.RoleID == user.Id
                            orderby ur.Id
                            select ur;
                return _staticCacheManager.Get(key, () => query.ToList());

            }
            catch (Exception ex)
            {
                _logger.Error("GetCustomerRoles error",ex);
                return null;
            }
        }

        public int[] GetCustomerRoleIds(Auth_User user, bool showHidden = false)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            try
            {
                var key = _cacheKeyService.PrepareKeyForShortTermCache(MotelUserServicesDefaults.UserRoleIdsCacheKey, user);

                var query = from ur in  _userRolesMappingRepository.Table join urm in _userRolesMappingRepository.Table
                            on ur.Id equals  urm.RoleID
                            where urm.RoleID == user.Id
                            orderby ur.Id
                            select ur.RoleID;
                return _staticCacheManager.Get(key, () => query.ToArray());

            }
            catch (Exception ex)
            {
                _logger.Error("GetCustomerRoles error",ex);
                return null;
            }
        }

        public void AddUserRoleMapping(Auth_UserRoles roleMapping)
        {
            if (roleMapping == null)
                throw new ArgumentNullException(nameof(roleMapping));

            _userRolesMappingRepository.Insert(roleMapping);

            _eventPublisher.EntityInserted(roleMapping);  

        }

        public void RemoveUserRoleMapping(Auth_UserRoles roleMapping)
        {
            if (roleMapping == null)
                throw new ArgumentNullException(nameof(roleMapping));

            _userRolesMappingRepository.Delete(roleMapping);
            _eventPublisher.EntityDeleted(roleMapping);  
        }

       
        #endregion

        #region User Permisson
        public IList<Auth_Assign> GetAllPermissonOfUser(int Id)
        {
            var key = _cacheKeyService.PrepareKeyForDefaultCache(MotelSecurityDefaults.PermissionsAllowedCacheKey, ObjectTypeEnum.User,Id);
            var pObjectType =  SqlParameterHelper.GetInt32Parameter("@_ObjectType",(int)ObjectTypeEnum.User);
            var pUserId=  SqlParameterHelper.GetInt32Parameter("@_Id", Id);
             var permission = _permissionAssign.EntityFromSql("GetAllPermissonOfUser",pObjectType,pUserId);
            return _staticCacheManager.Get(key,() => permission);
        }
        public IList<Auth_Assign> GetAllPermissonOfUser(Auth_User user)
        {
            return GetAllPermissonOfUser(user.Id);
        }

        public Auth_User GetUserById(int Id)
        {
           return _userRepository.GetById(Id);
        }



        #endregion
    }
}
