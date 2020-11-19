using Motel.Domain.Domain.Media;
using Motel.Domain.Domain.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Api.Framework.Response
{
    using  Motel.Domain.Domain.UtilitiesRoom;
    public class ResponseDetailPage
    {
        public ResponseDetailPage()
        {
            Post = new RentalPost();
            pictures = new List<Picture>();
        }
        public RentalPost Post { get; set;}

        public string WardName {get;set;}
		public string ProvincialName {get;set;}
		public string DistrictName {get;set;}
		
        public List<Picture> pictures  {get;set;}
        public List<UtilitiesRoom> utilitiesRooms  {get;set;}
    }
}
