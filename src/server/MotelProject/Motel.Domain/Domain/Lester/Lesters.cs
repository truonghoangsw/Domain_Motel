using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Lester
{
	public class Lesters:BaseEntity
	{
		public string Mobile {get;set;}
		public string Salt {get;set;}
		public string Password {get;set;}
		public DateTime Birthday {get;set;}=CommonHelper.DateTimeDefault();
		public string FullName {get;set;}
		public int Gender {get;set;}
		public string Address {get;set;}
		public byte AddressType {get;set;}
		public string AccountName {get;set;}
		public byte Deleted {get;set;}
		public DateTime CreatedTime {get;set;}=CommonHelper.DateTimeDefault();
		public DateTime UpdatedTime {get;set;}=CommonHelper.DateTimeDefault();
		public string IdentityCard {get;set;}
		public DateTime IdentityDay {get;set;}=CommonHelper.DateTimeDefault();
		public byte StatusId {get;set;}
		public int UserId {get;set;}
		public string FacebookId {get;set;}

	
	}
}
