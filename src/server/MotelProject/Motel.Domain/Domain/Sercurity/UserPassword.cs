using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Sercurity
{
    public class UserPassword
    {
        public int UserId { get; set; }

        public string PasswordHash { get; set; }

    }
}
