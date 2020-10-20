using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Sercurity
{
    public class Auth_UsersLog: BaseEntity
    {
        public int UserLogID {get;set;}
		public int EventID {get;set;}
		public int UserID {get;set;}
		public string ObjectCode {get;set;}
		public int EntityID {get;set;}
		public int EntityType {get;set;}
		public string EntityTypeName {get;set;}
		public DateTime CreatedTime {get;set;}
		public int Result {get;set;}
		public string Message {get;set;}
		public string Infomation {get;set;}
		public string IP {get;set;}


    }
}
