using Motel.Core;
using Motel.Domain.Domain.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Api.Framework.Response
{
    public class ResponsePostGet : IResponse
    {
        PagedList<RentalPost> rentalPosts { get; set;}
        public int MessageCode{get;set;}
        public string Message{get;set;}
    }
}
