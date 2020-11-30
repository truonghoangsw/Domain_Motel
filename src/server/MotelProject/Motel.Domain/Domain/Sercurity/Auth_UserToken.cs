using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Sercurity
{
    public class Auth_UserToken:BaseEntity
    {
        public string AccessTokenHash {get;set;}
        public DateTime AccessTokenExpiresDateTime {get;set;}
        public string RefreshTokenIdHash {get;set;}
        public string RefreshTokenIdHashSource {get;set;}
        public DateTime RefreshTokenExpiresDateTime {get;set;}
        public int UserId {get;set;}
    }
}
