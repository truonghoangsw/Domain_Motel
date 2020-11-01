using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Retener
{
    public enum CustomerNameFormat
    {
        /// <summary>
        /// Show emails
        /// </summary>
        ShowEmails = 1,

        /// <summary>
        /// Show usernames
        /// </summary>
        ShowUsernames = 2,

        /// <summary>
        /// Show full names
        /// </summary>
        ShowFullNames = 3,

        /// <summary>
        /// Show first name
        /// </summary>
        ShowFirstName = 10
    }
}
