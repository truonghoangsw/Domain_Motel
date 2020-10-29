using Motel.Domain.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Authentication
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="isPersistent">Whether the authentication session is persisted across multiple requests</param>
        void SignIn(Auth_User user, bool isPersistent);

        /// <summary>
        /// Sign out
        /// </summary>
        void SignOut();

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <returns>Customer</returns>
        Auth_User GetAuthenticatedCustomer();
    }
}
