using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.IInstallationService
{
    public partial  interface IInstallationService
    {
        /// <summary>
        /// Install required data
        /// </summary>
        /// <param name="defaultUserEmail">Default user email</param>
        /// <param name="defaultUserPassword">Default user password</param>
        void InstallRequiredData(string defaultUserEmail, string defaultUserPassword);
        
        /// <summary>
        /// Install sample data
        /// </summary>
        /// <param name="defaultUserEmail">Default user email</param>
        void InstallSampleData(string defaultUserEmail);
    }
}
