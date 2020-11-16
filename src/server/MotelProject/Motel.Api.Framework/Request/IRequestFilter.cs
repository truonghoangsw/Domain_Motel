using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Api.Framework.Request
{
    public interface IRequestFilter
    {
        int? PageIndex { get; set;}
        int? PageSize{ get; set;}
    }
}
