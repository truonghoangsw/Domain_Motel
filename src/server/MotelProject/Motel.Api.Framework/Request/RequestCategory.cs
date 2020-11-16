using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Api.Framework.Request
{
    public class RequestCategory : IRequestFilter
    {
        public int? PirceMax { get; set;}
        public int? PirceLow { get; set;}
        public string? Name { get; set;}
        public int? PageIndex {get;set;}
        public int? PageSize {get;set;}
    }
}
