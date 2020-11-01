using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Post
{
    public class PackageTypePost
    {
        public int Id {get;set;}
		public string NamePackage {get;set;}
		public double PricePerDay {get;set;}
		public int AmountDay {get;set;}
		public string TypePackage {get;set;}
		public int CreateBy {get;set;}
		public DateTime CreatedTime {get;set;}

    }
}
