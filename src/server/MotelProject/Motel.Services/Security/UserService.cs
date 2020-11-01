using Motel.Core;
using Motel.Core.Caching;
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
        private readonly IRepository<Auth_UserRoles> _userRolesMappingRepository;
        private readonly IRepository<Auth_Roles> _userRoleRepository;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ILogger _logger;
        #endregion

        #region Ctor
        public UserService(CachingSettings cachingSettings,
            UserSettings userSettings,
            ICacheKeyService cacheKeyService,
            IEventPublisher eventPublisher,
            IRepository<Auth_User> userRepository,
            IRepository<Auth_UserRoles> userRolesMappingRepository,
            IRepository<Auth_Roles> userRoleRepository,
            IStaticCacheManager staticCacheManager,
            ILogger logger)
        {
            _cachingSettings = cachingSettings;
            _userSettings = userSettings;
            _cacheKeyService = cacheKeyService;
            _eventPublisher = eventPublisher;
            _userRolesMappingRepository = userRolesMappingRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _staticCacheManager = staticCacheManager;
            _logger = logger;
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

        public IList<Auth_Roles> GetUserRoles(Auth_User user, bool showHidden = false)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            try
            {
                   var query = from ur in  _userRoleRepository.Table join urm in _userRolesMappingRepository.Table
                               on ur.Id equals  urm.RoleID
                               where urm.RoleID == user.Id
                               orderby ur.Id
                               select ur;
                    var key = _cacheKeyService.PrepareKeyForShortTermCache(MotelUserServicesDefaults.UserRoleIdsCacheKey, user, showHidden);
                return query.ToCachedList(key);

            }
            catch (Exception ex)
            {
                _logger.Error("GetCustomerRoles error",ex);
                return null;
            }
        }

        public void UpdateCustomerPassword(UserPassword UserPassword)
        {
            if (UserPassword == null)
                throw new ArgumentNullException(nameof(UserPassword));
        }
    }
}
