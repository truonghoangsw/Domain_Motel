using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Post
{
    public class PostCategoryMapping:BaseEntity
    {
	    public int CategoryId {get;set;}
	    public int RentalPostId {get;set;}
	    public bool IsFeaturedProduct {get;set;}
	    public int DisplayOrder {get;set;}
    }
}
