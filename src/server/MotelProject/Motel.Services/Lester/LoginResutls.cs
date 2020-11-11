using Motel.Core.Enum;
using Motel.Domain;
using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Sercurity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Lester
{
    public class LoginResutls
    {
        public Auth_User User { get;set;}
        public CustomPrincipal customPrincipal { get;set;}
        public MessgeCodeRegistration MessageCode { get;set;}
    }
}
