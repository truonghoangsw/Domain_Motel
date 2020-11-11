using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Sercurity
{
    public class Auth_UserToken:BaseEntity
    {
        public string AccessTokenHash {get;set;}
        public DateTimeOffset AccessTokenExpiresDateTime {get;set;}
        public string RefreshTokenIdHash {get;set;}
        public string RefreshTokenIdHashSource {get;set;}
        public DateTimeOffset RefreshTokenExpiresDateTime {get;set;}
        public int UserId {get;set;}
    }
}
