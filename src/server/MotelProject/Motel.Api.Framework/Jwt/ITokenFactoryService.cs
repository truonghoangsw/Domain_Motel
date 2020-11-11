using Motel.Domain.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Motel.Api.Framework.Jwt
{
    public interface ITokenFactoryService
    {
        JwtTokensData CreateJwtTokensAsync(Auth_User user);
        string GetRefreshTokenSerial(string refreshTokenValue);
    }
}
