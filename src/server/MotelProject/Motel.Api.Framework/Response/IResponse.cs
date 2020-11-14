using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Api.Framework.Response
{
    public interface IResponse
    {
        public int MessageCode { get; set;}
        public string Message { get; set;}
    }
}
