using Motel.Core;
using System;

namespace Motel.Domain.Domain.Post
{
    public class RentalPost:BaseEntity
    {
		public string TitlePost {get;set;}
		public double LengthMotel {get;set;}
		public double WideMotel {get;set;}
		public int MonthlyPrice {get;set;}
		public int Acreage {get;set;}
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
		public DateTime ExpirationDate {get;set;} = CommonHelper.DateTimeDefault();
		public DateTime RenewalDate {get;set;} =CommonHelper.DateTimeDefault();
		public byte TypeGendeRroom {get;set;}
		public int ElectricityBill {get;set;}
		public int WaterBill {get;set;}
		public int MotelTypeId {get;set;}
		public int InternetMoney {get;set;}
		public int ServiceFee {get;set;}
		public int CategoryId {get;set;}
		public int RoomNumber {get;set;}
		public string Parking {get;set;}
		public int WardId {get;set;}
		public string AddressDetail {get;set;}
		public int ProvincialId {get;set;}
		public int DistrictId {get;set;}

    }
}
