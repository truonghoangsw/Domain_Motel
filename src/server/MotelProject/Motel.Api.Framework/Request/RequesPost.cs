using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Api.Framework.Request
{
    public class RequesPost: IRequestFilter
    {
        public int[] UtilitieIds { get;set;}

        public int[] CatalogIds { get;set;}
        public string TitlePost {get;set;}
        public int? ToMonthlyPrice {get;set;}
        public int? FromMonthlyPrice {get;set;}
        public int? NumberRoom {get;set;}
        public string Address {get;set;}
        public int? PageIndex {get;set;} = 0;
        public int? PageSize {get;set;} = int.MaxValue;
    }
}
