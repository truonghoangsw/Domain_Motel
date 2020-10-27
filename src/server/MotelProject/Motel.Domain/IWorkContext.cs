using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Sercurity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain
{
    public interface IWorkContext
    {
        Auth_User CurrentUser { get; set; }
    }
}
