using Motel.Core.Caching;
using Motel.Domain.ContextDataBase;
using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Sercurity;
using Motel.Services.Caching;
using Motel.Services.Events;
using Motel.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Motel.Services.Security
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly CachingSettings _cachingSettings;
        private readonly UserSettings _customerSettings;
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
            UserSettings customerSettings,
            ICacheKeyService cacheKeyService,
            IEventPublisher eventPublisher,
            IRepository<Auth_User> userRepository,
            IRepository<Auth_UserRoles> userRolesMappingRepository,
            IRepository<Auth_Roles> userRoleRepository,
            IStaticCacheManager staticCacheManager,
            ILogger logger)
        {
            _cachingSettings = cachingSettings;
            _customerSettings = customerSettings;
            _cacheKeyService = cacheKeyService;
            _eventPublisher = eventPublisher;
            _userRolesMappingRepository = userRolesMappingRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _staticCacheManager = staticCacheManager;
            _logger = logger;
        }
        #endregion
       
        public IList<Auth_UserRoles> GetCustomerRoles(Auth_User user, bool showHidden = false)
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
                    var key = _cacheKeyService.PrepareKeyForShortTermCache(MotelCustomerServicesDefaults.CustomerRolesCacheKey, customer, showHidden);


            }
            catch (Exception ex)
            {
                _logger.Error("GetCustomerRoles error",ex);
            }
        }
    }
}
