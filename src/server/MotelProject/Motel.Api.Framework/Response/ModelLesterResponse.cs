using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Api.Framework.Response
{
    public class ModelLesterResponse
    {
        public string FullName { get; set; }
        public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirDay { get; set; }
        public int IdentityCard { get; set; }

    }
}
