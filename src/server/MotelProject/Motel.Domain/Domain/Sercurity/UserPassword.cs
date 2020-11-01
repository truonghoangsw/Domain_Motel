using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Sercurity
{
    public class UserPassword: BaseEntity
    {
        
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime CreatedOnUtc { get; set; }

    }
}
