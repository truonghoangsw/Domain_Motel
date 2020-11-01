using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Post
{
    public class PostPictureMaping:BaseEntity
    {
	    public int PictureId {get;set;}
	    public int PostId {get;set;}
	    public int DisplayOrder {get;set;}
    }
}
