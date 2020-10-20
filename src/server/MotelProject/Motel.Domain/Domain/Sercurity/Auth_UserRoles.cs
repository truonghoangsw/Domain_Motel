using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Sercurity
{
    public class Auth_UserRoles: BaseEntity
    {
        public int UserID {get;set;}
	    public int RoleID {get;set;}
    }
}
