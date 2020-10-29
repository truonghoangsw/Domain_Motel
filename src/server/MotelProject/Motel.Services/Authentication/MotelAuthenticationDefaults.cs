using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Authentication
{
    public class MotelAuthenticationDefaults
    {
        public static string AuthenticationScheme => "Authentication";

        public static string ClaimsIssuer => "motelCommerce";
        public static PathString LoginPath => new PathString("/login");
        public static PathString LogoutPath => new PathString("/logout");
        public static string ReturnUrlParameter => string.Empty;
        public static string ExternalAuthenticationErrorsSessionKey => "motel.externalauth.errors";
        public static PathString AccessDeniedPath => new PathString("/page-not-found");


    }
}
