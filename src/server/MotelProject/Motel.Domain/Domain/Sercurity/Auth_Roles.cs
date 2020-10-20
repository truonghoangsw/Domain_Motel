using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Sercurity
{
   public class Auth_Roles: BaseEntity
    {
        public string Name {get;set;}
        public string Desc {get;set;}
        public byte Status {get;set;}
        public int CreatedBy {get;set;}
        public DateTime CreatedTime {get;set;}
        public int UpdatedBy {get;set;}
        public DateTime UpdatedTime {get;set;}
        public byte Deleted {get;set;}
        public byte IsSysAdmin {get;set;}
        public byte IsShow {get;set;}

    }
}
