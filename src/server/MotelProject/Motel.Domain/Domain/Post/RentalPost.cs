using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Post
{
    public class RentalPost:BaseEntity
    {
		public string TitlePost {get;set;}
		public double LengthMotel {get;set;}
		public double WideMotel {get;set;}
		public int MonthlyPrice {get;set;}
		public string AcreageDram {get;set;}
		public string DescriptionInformation {get;set;}
		public int TypeRoomToilet {get;set;}
		public DateTime CreateDate {get;set;}
		public int CreateBy {get;set;}
		public DateTime UpdateDate {get;set;}
		public byte Status {get;set;}
		public string FurnitureInformation {get;set;}
		public int TerritoriesId {get;set;}
		public int LesterId {get;set;}
		public string Tag {get;set;}
		public int PackageTypePostId {get;set;}
		public DateTime ExpirationDate {get;set;}
		public DateTime RenewalDate {get;set;}
		public byte TypeGendeRroom {get;set;}
		public int ElectricityBill {get;set;}
		public int WaterBill {get;set;}
		public int MotelTypeId {get;set;}
		public int InternetMoney {get;set;}
		public int ServiceFee {get;set;}

    }
}
