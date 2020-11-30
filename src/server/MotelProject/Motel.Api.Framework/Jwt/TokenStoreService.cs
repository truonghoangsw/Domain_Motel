using LinqToDB;
using Microsoft.Extensions.Options;
using Motel.Core.Caching;
using Motel.Domain.ContextDataBase;
using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Sercurity;
using Motel.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motel.Api.Framework.Jwt
{
    public class TokenStoreService : ITokenStoreService
    {
        #region Fields
        protected readonly IStaticCacheManager _staticCacheManager;
        protected readonly IRepository<Auth_UserToken> _tokenUserRepository;
        protected readonly IRepository<Auth_User> _userRepository;
        protected readonly IUserService _userServices;
        protected readonly ITokenFactoryService _tokenFactoryService;
        protected readonly IEncryptionService _encryptionService;
        private readonly IOptionsSnapshot<BearerTokensOptions> _configuration;
        #endregion

        #region Ctor
        public TokenStoreService(
            IRepository<Auth_UserToken> tokenUserRepository, 
            IUserService userServices,
            IStaticCacheManager staticCacheManager,
            ITokenFactoryService tokenFactoryService,
            IEncryptionService encryptionService,
            IOptionsSnapshot<BearerTokensOptions> configuration
        ){
            _tokenUserRepository = tokenUserRepository;
            _userServices = userServices;
            _staticCacheManager = staticCacheManager;
            _tokenFactoryService = tokenFactoryService;
            _encryptionService=encryptionService;
            _configuration = configuration;
        }

    
        #endregion

        #region Method
        public void AddUserToken(Auth_UserToken userToken)
        {
            if (!_configuration.Value.AllowMultipleLoginsFromTheSameUser)
            {
                 InvalidateUserTokens(userToken.UserId);
            }
             DeleteTokensWithSameRefreshTokenSource(userToken.RefreshTokenIdHashSource);
            _tokenUserRepository.Insert(userToken);
        }

        public void AddUserToken(Auth_User user, string refreshTokenSerial, string accessToken, string refreshTokenSourceSerial)
        {
            var now = DateTime.UtcNow;
            var token = new Auth_UserToken
            {
                UserId = user.Id,
                // Refresh token handles should be treated as secrets and should be stored hashed
                RefreshTokenIdHash = _encryptionService.GetSha256Hash(refreshTokenSerial),
                RefreshTokenIdHashSource = string.IsNullOrWhiteSpace(refreshTokenSourceSerial) ?
                                           null : _encryptionService.GetSha256Hash(refreshTokenSourceSerial),
                AccessTokenHash = _encryptionService.GetSha256Hash(accessToken),
                RefreshTokenExpiresDateTime = now.AddMinutes(_configuration.Value.RefreshTokenExpirationMinutes),
                AccessTokenExpiresDateTime = now.AddMinutes(_configuration.Value.AccessTokenExpirationMinutes)
            };
             AddUserToken(token);
        }

        public void DeleteExpiredTokens()
        {
            var now = DateTimeOffset.UtcNow;
             _tokenUserRepository.Table.Where(x => x.RefreshTokenExpiresDateTime < now)
                        .ForEachAsync(userToken => _tokenUserRepository.Delete(userToken));
        }

        public void DeleteToken(string refreshTokenValue)
        {
            var token =  FindToken(refreshTokenValue);
            if (token != null)
            {
                _tokenUserRepository.Delete(token);
            }
        }

        public void DeleteTokensWithSameRefreshTokenSource(string refreshTokenIdHashSource)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenIdHashSource))
            {
                return;
            }

             _tokenUserRepository.Table.Where(t => t.RefreshTokenIdHashSource == refreshTokenIdHashSource ||
                                     t.RefreshTokenIdHash == refreshTokenIdHashSource &&
                                      t.RefreshTokenIdHashSource == null)
                .ForEachAsync(userToken => _tokenUserRepository.Delete(userToken));
        }

        public Auth_UserToken FindToken(string refreshTokenValue)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenValue))
            {
                return null;
            }
            var refreshTokenSerial = _tokenFactoryService.GetRefreshTokenSerial(refreshTokenValue);
            if (string.IsNullOrWhiteSpace(refreshTokenSerial))
            {
                return null;
            }

            var refreshTokenIdHash = _encryptionService.GetSha256Hash(refreshTokenSerial);
            
            return _tokenUserRepository.Table.FirstOrDefault(x=>x.RefreshTokenIdHash == refreshTokenIdHash);
        }

        public void InvalidateUserTokens(int userId)
        {
            _tokenUserRepository.Table.Where(x => x.UserId == userId)
                        .ForEachAsync(userToken => _tokenUserRepository.Delete(userToken));
        }

        public bool IsValidToken(string accessToken, int userId)
        {
            var accessTokenHash = _encryptionService.GetSha256Hash(accessToken);
            var userToken =  _tokenUserRepository.Table.FirstOrDefault(x=>x.AccessTokenHash == accessTokenHash && x.UserId == userId);
             return userToken?.AccessTokenExpiresDateTime >= DateTime.UtcNow;
        }

        public void RevokeUserBearerTokens(string userIdValue, string refreshTokenValue)
        {
            if (!string.IsNullOrWhiteSpace(userIdValue) && int.TryParse(userIdValue, out int userId))
            {
                if (_configuration.Value.AllowSignoutAllUserActiveClients)
                {
                     InvalidateUserTokens(userId);
                }
            }
        }
        #endregion

    }
}
