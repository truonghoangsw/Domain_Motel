using LinqToDB.Mapping;
using Motel.Core;
using Motel.Domain.Domain.Media;
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

        [Association(ThisKey="PostId", OtherKey="Id")]
        public virtual  RentalPost rentalPost { get; set;}
         [Association(ThisKey="PictureId", OtherKey="Id")]
        public virtual  Picture Picture { get; set;}
    }
}
