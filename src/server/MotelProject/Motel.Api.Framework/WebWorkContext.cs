using Motel.Domain;
using Motel.Domain.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Api.Framework
{
    public class WebWorkContext : IWorkContext
    {
        public Auth_User CurrentUser { get;set;}
    }
}
