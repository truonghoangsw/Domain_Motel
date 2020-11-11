using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Sercurity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Motel.Api.Framework.Jwt
{
    public interface  ITokenStoreService
    {
        void AddUserToken(Auth_UserToken userToken);
        void AddUserToken(Auth_User user, string refreshTokenSerial, string accessToken, string refreshTokenSourceSerial);
        bool IsValidToken(string accessToken, int userId);
        void DeleteExpiredTokens();
        Auth_UserToken FindToken(string refreshTokenValue);
        void DeleteToken(string refreshTokenValue);
        void DeleteTokensWithSameRefreshTokenSource(string refreshTokenIdHashSource);
        void InvalidateUserTokens(int userId);
        void RevokeUserBearerTokens(string userIdValue, string refreshTokenValue);
    }
}
