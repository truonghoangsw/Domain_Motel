using Microsoft.AspNetCore.Authentication.JwtBearer;
using Motel.Domain;
using Motel.Services.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Motel.Api.Framework.Jwt
{

    public interface ITokenValidatorService
    {
        Task ValidateAsync(TokenValidatedContext context);
    }

    public class TokenValidatorService : ITokenValidatorService
    {
        private readonly IUserService _usersService;
        private readonly ITokenStoreService _tokenStoreService;
        private readonly IWorkContext _workContext;

        public TokenValidatorService(
            IUserService usersService, 
            ITokenStoreService tokenStoreService,
            IWorkContext workContext)
        {
            _usersService = usersService;

            _tokenStoreService = tokenStoreService;
            _workContext = workContext;
        }

        public async Task ValidateAsync(TokenValidatedContext context)
        {
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any())
            {
                context.Fail("This is not our issued token. It has no claims.");
                return;
            }

            var serialNumberClaim = claimsIdentity.FindFirst(ClaimTypes.SerialNumber);
            if (serialNumberClaim == null)
            {
                context.Fail("This is not our issued token. It has no serial.");
                return;
            }

            var userIdString = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;
            if (!int.TryParse(userIdString, out int userId))
            {
                context.Fail("This is not our issued token. It has no user-id.");
                return;
            }

            var user =  _usersService.GetUserById(userId);
            if (user == null || user.Deleted  != 0)
            {
                // user has changed his/her password/roles/stat/IsActive
                context.Fail("This token is expired. Please login again.");
            }
            if (user.LockoutEnabled)
            {
                  context.Fail("Tài khoản đã bị bán");
            }
            if (!(context.SecurityToken is JwtSecurityToken accessToken) || string.IsNullOrWhiteSpace(accessToken.RawData) ||
                ! _tokenStoreService.IsValidToken(accessToken.RawData, userId))
            {
                context.Fail("This token is not in our database.");
                return;
            }
            AccessControl.User = user;
        }
    }

}
