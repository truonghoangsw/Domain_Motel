using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Lester
{
    public class RegistrationLesterRequest
    {
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set;}
        public string Avatar { get; set; }
        public string IdentityCard { get; set;}
        public DateTime IdentityDay { get; set;}
        public string UserName { get; set;}
        public string FullName { get; set;}
        public int TerritoriesId { get; set;}
       
    }
}
