using Motel.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Lester
{
    public class LesterSettings: ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether usernames are used instead of emails
        /// </summary>
        public bool UsernamesEnabled { get; set; }

        public bool CheckUsernameAvailabilityEnabled { get; set; }

        public bool AllowUsersToChangeUsernames { get; set; }
        public bool UsernameValidationEnabled { get; set; }
        public bool UsernameValidationUseRegex { get; set; }
        public string UsernameValidationRule { get; set; }
        public string HashedPasswordFormat { get; set; }
        public int PasswordMinLength { get; set; }
        public bool PasswordRequireDigit { get; set; }
    }
}
