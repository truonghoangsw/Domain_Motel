using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Api.Framework.Response
{
    public class ImageProduct
    {
        [FromForm(Name="files")]
        public IFormFile files { get;set;}
    }
}
