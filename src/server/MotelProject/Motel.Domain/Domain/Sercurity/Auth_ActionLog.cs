using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Sercurity
{
    public class Auth_ActionLog: BaseEntity
	{
		public string Action {get;set;}
		public int UserID {get;set;}
		public int ObjectID {get;set;}
		public int ObjectType {get;set;}
		public string Data {get;set;}
		public DateTime CreatedTime {get;set;}
		public string Ip {get;set;}
		public int Result {get;set;}
		public string Message {get;set;}
		public string RequestUrl {get;set;}
		public string Device {get;set;}
		public string HttpMethod {get;set;}

	}
}
