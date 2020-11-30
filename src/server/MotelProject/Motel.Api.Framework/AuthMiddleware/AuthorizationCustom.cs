using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Motel.Core;
using Motel.Domain;
using Motel.Services.Logging;
using Motel.Services.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace Motel.Api.Framework.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizationCustomFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var _context = context.HttpContext;
            ClaimsIdentity principal  =  _context.User.Identity as ClaimsIdentity;
            var router = _context.Request.RouteValues;
            string[] permissonOfAction = CommonHelper.GenericPermissonOfControler(router);
            if(permissonOfAction == null)
            {
                context.Result =new UnauthorizedResult();
                return;
            }
            int userId = 0;
            var logger =  context.HttpContext.RequestServices.GetRequiredService<ILogger>();
            var objectId = principal.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.UserData).Value;
            if(!int.TryParse(objectId?.ToString(),out userId))
            {
                context.Result =new UnauthorizedResult();
                logger.Error("Authorization User Data is not found");
                return;
            }
            var permissonUser = context.HttpContext.RequestServices.GetRequiredService<IPermissionService>();
            if(AccessControl.User  == null)
            {
                var userServices = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                var user = userServices.GetUserById(userId);
                AccessControl.User = user;
            }
            if(!(permissonUser.Authorize(permissonOfAction[0],AccessControl.User) || permissonUser.Authorize(permissonOfAction[1],AccessControl.User)))
            {
                context.Result =new UnauthorizedResult();
                logger.Error("Authorization User Data is not found");
                return;
            }
        }

    }

}
