using Motel.Domain;
using Motel.Domain.Domain.Sercurity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Lester
{
    public class LoginResutls
    {
        public CustomPrincipal customPrincipal { get;set;}
        int MessageCode { get;set;}
    }
}
