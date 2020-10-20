using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Auth
{
    public class Auth_Permission: BaseEntity
	{
		public string Permission {get;set;}
		public string Name {get;set;}
		public byte Status {get;set;}
		public int CreatedBy {get;set;}
		public DateTime CreatedTime {get;set;}
		public int UpdatedBy {get;set;}
		public DateTime UpdatedTime {get;set;}
		public byte Deleted {get;set;}

	
	}
}
