using Motel.Domain;
using Motel.Domain.Domain.Auth;
using Motel.Domain.Domain.Lester;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Lester
{
    public interface ILesterRegistrationService
    {
        Lesters GetFacebookId(string facebookId);
        LoginResutls RegistrationFacebook(RequestLoginFacebook loginFacebook);
        void LockOut( int userId);
        void ResetAccount( int userId);
        Auth_User UserExists( string userName);
        LoginResutls Login(string userName,string password);
        RegistrationLeterReults Registration(RegistrationLesterRequest lesterModel);
    }
}
