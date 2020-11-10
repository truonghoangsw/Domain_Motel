using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Motel.Domain.Domain.Sercurity
{
    public class CustomPrincipal : IPrincipal
    {
        public bool HasPermission(string permission)
        {
            if (Permissions.Any(r => permission.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsInRole(string role)
        {
            if (Roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal()
        {
        }
       

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string[] Permissions { get; set; }
        public string[] Roles { get; set; }
        public bool IsSysAdmin { get; set; }
        public bool TwoFactorEnabled { get; set; }
        /// <summary>
        /// TokenString sử dụng trong tính năng callcenter, sau khi đăng nhập xong sẽ thiết lập luôn
        /// </summary>
        public string TokenString { get; set; }
    }
}
