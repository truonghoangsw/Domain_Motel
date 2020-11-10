using Motel.Domain;
using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Sercurity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Api.Framework
{
    public class WebWorkContext : IWorkContext
    {
        public Auth_User CurrentUser { get;set;}
        public CustomPrincipal CustomPrincipal { get;set;}
    }
}
