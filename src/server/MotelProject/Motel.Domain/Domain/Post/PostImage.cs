using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Post
{
    public class PostImage:BaseEntity
    {
        public int Id {get;set;}
        public int PostId {get;set;}
        public string Path {get;set;}
        public DateTime CreateTime {get;set;}
        public int CreateBy {get;set;}
        public DateTime UpdateTime {get;set;}
        public byte Status {get;set;}
    }
}
