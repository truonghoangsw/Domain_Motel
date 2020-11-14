using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Lester
{
    public class RequestLoginFacebook
    {
        public string name { get; set;}
        public string phone { get; set;}
        public string email { get; set; }
        public string avatar { get; set; }
        public string facebookId { get; set; }
    }
}
