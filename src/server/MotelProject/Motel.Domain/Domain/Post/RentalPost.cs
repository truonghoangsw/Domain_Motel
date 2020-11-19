using LinqToDB.Mapping;
using Motel.Core;
using Motel.Domain.Domain.Media;
using System;
using System.Collections.Generic;

namespace Motel.Domain.Domain.Post
{
	using Motel.Domain.Domain.UtilitiesRoom;
    public class RentalPost:BaseEntity
    {
			public string TitlePost {get;set;}
			public int MonthlyPrice {get;set;}
			public int Acreage {get;set;}
			public string DescriptionInformation {get;set;}
			public DateTime CreateDate {get;set;}
			public int CreateBy {get;set;}
			public DateTime UpdateDate {get;set;} = CommonHelper.DateTimeDefault();
			public byte Status {get;set;}
			public int TerritoriesId {get;set;}
			public int LesterId {get;set;}
			public string Tag {get;set;}
			public int PackageTypePostId {get;set;}
			public DateTime ExpirationDate {get;set;} = CommonHelper.DateTimeDefault();
			public DateTime RenewalDate {get;set;} = CommonHelper.DateTimeDefault();
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
			public int Deposit {get;set;}
			public int NumberOfPeople {get;set;}
        public string NumberPhone { get; set; }

		public virtual IList<Picture> PostPictures { get; set; }
		public virtual IList<UtilitiesRoom> UtilitiesRooms { get; set; }


    }
}
