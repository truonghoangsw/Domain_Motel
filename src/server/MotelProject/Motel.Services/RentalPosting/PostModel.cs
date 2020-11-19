using Motel.Core.Enum;
using Motel.Domain.Domain.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.RentalPosting
{
    public class PostModel
    {
		public int[] UtilitiesIds { get; set; }
        public int[] PictureIds { get; set; }
        public string TitlePost {get;set;}
		public int? MonthlyPrice {get;set;}
		public int? Acreage  {get;set;}
		public string DescriptionInformation {get;set;}
		public int Status {get;set;}
		public int? TerritoriesId {get;set;}
		public int? LesterId {get;set;}
		public int? PackageTypePostId {get;set;}
		public byte? TypeGendeRroom {get;set;}
		public int? ElectricityBill {get;set;}
		public int? WaterBill {get;set;}
		public int? InternetMoney {get;set;}
		public int? CategoryId {get;set;}
		public int? ServiceFee {get;set;}
		public int? RoomNumber {get;set;}
		public string Parking { get; set;}
		public int? WardId {get;set;}
		public string AddressDetail {get;set;}
		public int? ProvincialId {get;set;}
		public int? DistrictId {get;set;}
        public string NumberPhone { get; set; }
		public int? Deposit {get;set;}
		public int? NumberOfPeople {get;set;}
        public RentalPost ConvertSetp(RentalPost rentalPost)
        {
			rentalPost.MonthlyPrice =  this.MonthlyPrice.HasValue ?  this.MonthlyPrice.Value:0;
			rentalPost.NumberPhone =  this.NumberPhone;
			rentalPost.Deposit =  this.Deposit.HasValue ?  this.Deposit.Value:0;
			rentalPost.NumberOfPeople =  this.NumberOfPeople.HasValue ?  this.NumberOfPeople.Value:0;
			rentalPost.CategoryId = this.CategoryId.HasValue ?  this.CategoryId.Value:0;
			rentalPost.RoomNumber = this.RoomNumber.HasValue ?   this.RoomNumber.Value:0;
			rentalPost.TypeGendeRroom = (byte)(this.TypeGendeRroom.HasValue? this.TypeGendeRroom.Value:0);
			rentalPost.Acreage = this.Acreage.HasValue? this.Acreage.Value:0;
			rentalPost.InternetMoney = this.InternetMoney.HasValue? this.InternetMoney.Value:0;
			rentalPost.ElectricityBill = this.ElectricityBill.HasValue? this.ElectricityBill.Value:0;
			rentalPost.WaterBill =  this.WaterBill.HasValue?  this.WaterBill.Value:0;
			rentalPost.Parking =  this.Parking;
			rentalPost.DistrictId = this.DistrictId.HasValue ? this.DistrictId.Value:0;
			rentalPost.ProvincialId = this.ProvincialId.HasValue ?  this.ProvincialId.Value:0 ;
			rentalPost.AddressDetail = this.AddressDetail;
			rentalPost.WardId =  this.WardId.HasValue?this.WardId.Value:0;
			rentalPost.DescriptionInformation = this.DescriptionInformation;
			rentalPost.TitlePost = this.TitlePost;
			return rentalPost;
        }
		
    }
}
