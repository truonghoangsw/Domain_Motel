using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motel.Api.Framework.Jwt;
using Motel.Api.Framework.Response;
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
        protected readonly ITokenStoreService _tokenStoreService;
        protected readonly ITokenFactoryService _tokenFactoryService;
        protected readonly IWorkContext _workContext;
        #endregion

        #region Ctor
         public LesterRegistrationController(
            ILesterRegistrationService lesterRegistration, 
            IUserService userService, 
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
            _workContext = workContext;
        }
        #endregion
        // GET: api/<LesterRegistrationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LesterRegistrationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LesterRegistrationController>
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost("[action]")]
        public IActionResult Registration([FromBody] string value)
        {
            return Ok();
        }

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
                return Ok(responseLogin);
            }
            else
            {
                responseLogin = new ResponseLogin(Core.Enum.MessgeCodeRegistration.NoData);
                return BadRequest(responseLogin);
            }
           
        }

        // PUT api/<LesterRegistrationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LesterRegistrationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
