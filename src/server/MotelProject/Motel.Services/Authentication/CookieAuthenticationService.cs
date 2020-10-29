using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Motel.Core.Enum;
using Motel.Domain.Domain.Auth;
using Motel.Services.Security;
using Motel.Services.Sercurity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Motel.Services.Authentication
{
    public class CookieAuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly UserSettings _userSettings;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Auth_User _cachedUser;

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
            if (_cachedUser != null)
                return _cachedUser;
                var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(MotelAuthenticationDefaults.AuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
                return null;
            Auth_User user =null;

            if (_userSettings.UsernamesEnabled)
            {
                //try to get customer by username
                var usernameClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name
                    && claim.Issuer.Equals(MotelAuthenticationDefaults.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
                if (usernameClaim != null)
                    user = _userService.GetUserByUsername(usernameClaim.Value);
            }
            else
            {
                //try to get customer by email
                var emailClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Email
                    && claim.Issuer.Equals(MotelAuthenticationDefaults.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
                if (emailClaim != null)
                    user = _userService.GetUserByEmail(emailClaim.Value);
                if (user == null || (user.Status != (int)EnumStatusUser.Approved)  || user.Deleted != (int)EnumStatusUser.Delete )
                 return null;
                
            }
            _cachedUser = user;

            return _cachedUser;

        }

        public  virtual async void SignIn(Auth_User user, bool isPersistent)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            var claims = new List<Claim>();
            if (!string.IsNullOrEmpty(user.UserName))
                claims.Add(new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String, MotelAuthenticationDefaults.ClaimsIssuer));
            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email, MotelAuthenticationDefaults.ClaimsIssuer));
            var userIdentity = new ClaimsIdentity(claims, MotelAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow
            };
            await _httpContextAccessor.HttpContext.SignInAsync(MotelAuthenticationDefaults.AuthenticationScheme, userPrincipal, authenticationProperties);

            //cache authenticated customer
            _cachedUser = user;

        }

         /// <summary>
        /// Sign out
        /// </summary>
        public virtual async void SignOut()
        {
            //reset cached customer
            _cachedUser = null;

            //and sign out from the current authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(MotelAuthenticationDefaults.AuthenticationScheme);
        }
      
    }
}
