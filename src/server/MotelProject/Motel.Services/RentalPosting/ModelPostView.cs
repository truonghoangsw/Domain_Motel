using Motel.Domain.Domain.Media;
using Motel.Domain.Domain.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Api.Services.RentalPosting
{
    public class ModelPostView
    {
        public RentalPost rentalPost { get; set;}
        public  IEnumerable<Picture> pictures { get; set;}
    }
}
