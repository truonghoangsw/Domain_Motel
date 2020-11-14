using Motel.Core;
using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Sercurity;
using System;
using System.Collections.Generic;

namespace Motel.Services.Security
{
    public interface IUserService
    {
        #region User Roles
        IList<Auth_UserRoles> GetUserRoles(Auth_User user, bool showHidden = false);

        int[] GetCustomerRoleIds(Auth_User user, bool showHidden = false);

        void AddUserRoleMapping(Auth_UserRoles roleMapping);

        void RemoveUserRoleMapping(Auth_UserRoles roleMapping);
        #endregion

        #region User permission
        IList<Auth_Assign> GetAllPermissonOfUser(int Id);
        IList<Auth_Assign> GetAllPermissonOfUser(Auth_User user);
        #endregion

        #region User
        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records</param>
        /// <param name="affiliateId">Affiliate identifier</param>
        /// <param name="vendorId">Vendor identifier</param>
        /// <param name="customerRoleIds">A list of customer role identifiers to filter by (at least one match); pass null or empty list in order to load all customers; </param>
        /// <param name="email">Email; null to load all customers</param>
        /// <param name="username">Username; null to load all customers</param>
        /// <param name="firstName">First name; null to load all customers</param>
        /// <param name="lastName">Last name; null to load all customers</param>
        /// <param name="dayOfBirth">Day of birth; 0 to load all customers</param>
        /// <param name="monthOfBirth">Month of birth; 0 to load all customers</param>
        /// <param name="company">Company; null to load all customers</param>
        /// <param name="phone">Phone; null to load all customers</param>
        /// <param name="zipPostalCode">Phone; null to load all customers</param>
        /// <param name="ipAddress">IP address; null to load all customers</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="getOnlyTotalCount">A value in indicating whether you want to load only total number of records. Set to "true" if you don't want to load data from database</param>
        /// <returns>Customers</returns>
        IPagedList<Auth_User> GetAllUser(DateTime? createdFromUtc = null, DateTime? createdToUtc = null,
            int[] customerRoleIds = null, string email = null, string username = null, string firstName = null, 
            string lastName = null, int dayOfBirth = 0, int monthOfBirth = 0,string phone = null, string zipPostalCode = null, string ipAddress = null, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
 
        Auth_User GetUserByUsername(string username);
        Auth_User GetUserById(int Id);
        Auth_User GetUserByEmail(string email);
        void UpdateCustomerPassword(UserPassword UserPassword);

        Auth_User InsertUserLester(Auth_User user);
        Auth_User InsertUserRetener(Auth_User user);
        Auth_User InsertUserAdmin(Auth_User user);
        void UpdateUser(Auth_User customer);

        #endregion
    }
}
