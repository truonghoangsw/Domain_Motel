using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Post
{
    public class PostComment:BaseEntity
    {
        public int Id {get;set;}
	    public int RetenerId {get;set;}
	    public string CommentText {get;set;}
	    public int PostId {get;set;}
	    public DateTime CreatedOnUtc {get;set;}
    }
}
