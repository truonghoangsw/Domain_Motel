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

    }
}
