using LinqToDB.Mapping;
using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Sercurity
{
    public class Auth_Assign: BaseEntity
    {
		public string Permission {get;set;}
		public int ObjectID {get;set;}
		[Column]
		public byte ObjectType {get;set;}
		public int CreatedBy {get;set;}
		public DateTime CreatedTime {get;set;}

    }
}
