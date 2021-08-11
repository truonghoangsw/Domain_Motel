using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motel.Api.Framework;
using Motel.Api.Framework.AuthMiddleware;
using Motel.Api.Framework.Jwt;
using Motel.Api.Framework.Middleware;
using Motel.Api.Framework.Response;
using Motel.Core;
using Motel.Core.Enum;
using Motel.Domain;
using Motel.Services.Lester;
using Motel.Services.Security;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Motel.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LesterRegistrationController : ControllerBase
    {
        #region Fields
        protected readonly ILesterRegistrationService _lesterRegistration;
        protected readonly IUserService _userService;
        protected readonly IPermissionService _permissionService;
        protected readonly IAntiForgeryCookieService _antiforgery;
        protected readonly ITokenStoreService _tokenStoreService;
        protected readonly ITokenFactoryService _tokenFactoryService;
        protected readonly IWorkContext _workContext;
        #endregion

        #region Ctor
         public LesterRegistrationController(
            ILesterRegistrationService lesterRegistration, 
            IUserService userService, 
            IAntiForgeryCookieService antiForgery,
            IPermissionService permissionService, 
            ITokenStoreService tokenStoreService, 
            ITokenFactoryService tokenFactoryService, 
            IWorkContext workContext)
        {
            _lesterRegistration = lesterRegistration;
            _userService = userService;
            _permissionService = permissionService;
            _tokenStoreService = tokenStoreService;
            _tokenFactoryService = tokenFactoryService;
            _antiforgery = antiForgery;
            _workContext = workContext;
        }
        #endregion
        // GET: api/<LesterRegistrationController>
        // GET api/<LesterRegistrationController>/5
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost("[action]")]
        public IActionResult Login([FromQuery] string userName,string password)
        {
            if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return BadRequest(new {messageCode = -100, message = "Dữ Liệu Sai"});
             var userResult =    _lesterRegistration.Login(userName,password);
            if(userResult.MessageCode == MessgeCodeRegistration.PasswordWrong)
            {
                return Unauthorized(new { messageCode = MessgeCodeRegistration.PasswordWrong, message = CommonHelper.DescriptionEnum(MessgeCodeRegistration.PasswordWrong)});
            }
            if(userResult.MessageCode != MessgeCodeRegistration.Suscess)
            {
                return Unauthorized(new { messageCode = userResult.MessageCode, message = CommonHelper.DescriptionEnum((MessgeCodeRegistration)userResult.MessageCode)});
            }
             var result =  _tokenFactoryService.CreateJwtTokensAsync(userResult.User);
             _tokenStoreService.AddUserToken(userResult.User, result.RefreshTokenSerial, result.AccessToken, null);
              _antiforgery.RegenerateAntiForgeryCookies(result.Claims);
             AccessControl.User = userResult.User;
             return Ok(new { access_token = result.AccessToken, refresh_token = result.RefreshToken });
        }

        // POST api/<LesterRegistrationController>
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost("[action]")]
        public IActionResult Registration([FromBody] RegistrationLesterRequest registrationModel)
        {
            if(registrationModel == null)
                  return BadRequest(new {messageCode = -100, message = "Dữ Liệu Sai"});
            var reults = _lesterRegistration.Registration(registrationModel);
            if(reults.MessageCode != MessgeCodeRegistration.Suscess)
                return BadRequest(new { messageCode =reults.MessageCode, message = CommonHelper.DescriptionEnum(reults.MessageCode)});
            AccessControl.User =_userService.GetUserById(reults.customPrincipal.UserId);
            return Ok();
        }

        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost("[action]")]
        public IActionResult RegistrationFacebook([FromBody]RequestLoginFacebook loginFacebook)
        {
            ResponseLogin responseLogin;
            if(loginFacebook != null)
            {
                LoginResutls resutlLogin = _lesterRegistration.RegistrationFacebook(loginFacebook);
                if (resutlLogin.MessageCode != Core.Enum.MessgeCodeRegistration.Suscess)
                {
                    responseLogin = new ResponseLogin(resutlLogin.MessageCode);
                   return BadRequest(responseLogin);
                }
                var result =  _tokenFactoryService.CreateJwtTokensAsync(resutlLogin.User);
                _tokenStoreService.AddUserToken(resutlLogin.User,result.RefreshToken,result.AccessToken,null);
                responseLogin = new ResponseLogin(Core.Enum.MessgeCodeRegistration.Suscess)
                {
                    access_token = result.AccessToken,
                    refresh_token = result.RefreshToken
                };
                AccessControl.User = resutlLogin.User;
                return Ok(responseLogin);
            }
            else
            {
                responseLogin = new ResponseLogin(Core.Enum.MessgeCodeRegistration.NoData);
                return BadRequest(responseLogin);
            }
           
        }

        [HttpPost("[action]")]
        public  IActionResult RefreshToken([FromBody]Token model)
        {
            var refreshTokenValue = model.RefreshToken;
            if (string.IsNullOrWhiteSpace(refreshTokenValue))
            {
                return BadRequest("refreshToken is not set.");
            }

            var token =  _tokenStoreService.FindToken(refreshTokenValue);
            if (token == null)
            {
                return Unauthorized();
            }
            var user = _userService.GetUserById(token.UserId);
            var result =  _tokenFactoryService.CreateJwtTokensAsync(user);
             _tokenStoreService.AddUserToken(user, result.RefreshTokenSerial, result.AccessToken, _tokenFactoryService.GetRefreshTokenSerial(refreshTokenValue));

            return Ok(new { access_token = result.AccessToken, refresh_token = result.RefreshToken });
        }
    }
}
