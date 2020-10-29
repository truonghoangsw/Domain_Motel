using Microsoft.AspNetCore.Http;
using Motel.Domain.Domain.Auth;
using Motel.Services.Security;
using Motel.Services.Sercurity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Authentication
{
    public class CookieAuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly UserSettings _userSettings;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Auth_User _cachedCustomer;

        #endregion

        #region Ctor

        public CookieAuthenticationService(UserSettings userSettings,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userSettings = userSettings;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion
        public Auth_User GetAuthenticatedCustomer()
        {
            throw new NotImplementedException();
        }

        public void SignIn(Auth_User user, bool isPersistent)
        {
            throw new NotImplementedException();
        }

        public void SignOut()
        {
            throw new NotImplementedException();
        }
    }
}
