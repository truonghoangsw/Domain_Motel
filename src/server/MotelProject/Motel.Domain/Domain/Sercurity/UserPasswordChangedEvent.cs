using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Sercurity
{
    public class UserPasswordChangedEvent
    {
        public UserPasswordChangedEvent(UserPassword password)
        {
            Password = password;
        }

        /// <summary>
        /// Customer password
        /// </summary>
        public UserPassword Password { get; }
    }

}
