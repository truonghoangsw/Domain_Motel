using LinqToDB.Mapping;
using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Territories
{
    public class Territories:BaseEntity
    {
		public byte RegionId {get;set;}
		public string Code {get;set;}
		public string Name {get;set;}
		public int Parent {get;set;}
		public int Order {get;set;}
		public string Slug {get;set;}
		[Column]
		public byte Status {get;set;}
		public int CreatedBy {get;set;}
		public DateTime CreatedTime {get;set;}
		public int UpdatedBy {get;set;}
		public DateTime UpdatedTime {get;set;}
		public byte Deleted {get;set;}
	
    }
}
