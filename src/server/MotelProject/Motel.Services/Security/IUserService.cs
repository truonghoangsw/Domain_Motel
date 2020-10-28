using Motel.Domain.Domain.Sercurity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Security
{
    public interface IUserService
    {
        IList<Auth_UserRoles> GetCustomerRoles(Auth_User user, bool showHidden = false);

    }
}
