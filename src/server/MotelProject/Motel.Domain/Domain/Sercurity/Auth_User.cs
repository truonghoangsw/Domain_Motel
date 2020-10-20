using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Auth
{
    public class Auth_User
	{
		public int UserID {get;set;}
		public string UserName {get;set;}
		public string NormalizedUserName {get;set;}
		public string Email {get;set;}
		public string NormalizedEmail {get;set;}
		public bool EmailConfirmed {get;set;}
		public string PasswordHash {get;set;}
		public string SecurityStamp {get;set;}
		public string ConcurrencyStamp {get;set;}
		public string PhoneNumber {get;set;}
		public bool PhoneNumberConfirmed {get;set;}
		public bool TwoFactorEnabled {get;set;}
		public DateTimeOffset LockoutEnd {get;set;}
		public bool LockoutEnabled {get;set;}
		public int AccessFailedCount {get;set;}
		public DateTime LastRequestUtc {get;set;}
		public int CreatedBy {get;set;}
		public DateTime CreatedTime {get;set;}
		public int UpdatedBy {get;set;}
		public DateTime UpdatedTime {get;set;}
		public byte Status {get;set;}
		public string Avatar {get;set;}
		public byte Deleted {get;set;}

	}
}
