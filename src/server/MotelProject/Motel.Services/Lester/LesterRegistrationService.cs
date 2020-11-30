using Motel.Core;
using Motel.Core.Enum;
using Motel.Domain;
using Motel.Domain.Domain.Auth;
using Motel.Services.Events;
using Motel.Services.Security;
using System;
using System.Security;
using System.Text;

namespace Motel.Services.Lester
{
    using Motel.Domain.ContextDataBase;
    using Motel.Domain.Domain.Lester;
    using Motel.Domain.Domain.Sercurity;
    using Motel.Services.Logging;
    using System.Linq;

    public class LesterRegistrationService : ILesterRegistrationService
    {

        #region Fields
        private readonly IUserService _userService;
        private readonly IRolesUserServices _rolesUserServices;
        private readonly IEncryptionService _encryptionService;
        private readonly IRepository<Lesters> _lestersRepository;
        private readonly ILogger _logger;

        #endregion

        #region Ctor
        public LesterRegistrationService(
            IUserService userService, 
            IRolesUserServices rolesUserServices, 
            IWorkContext workContext, 
            IEncryptionService encryptionService, 
           IRepository<Lesters> lestersRepository,
           ILogger logger)
        {
            _logger = logger;
            _lestersRepository = lestersRepository;
            _userService = userService;
            _rolesUserServices = rolesUserServices;
            _encryptionService = encryptionService;
        }
        #endregion

        #region Utilities
        RegistrationLeterReults IsValiDateRegistration(RegistrationLesterRequest lesterModel)
        {
            RegistrationLeterReults result =new RegistrationLeterReults();
            result.MessageCode = MessgeCodeRegistration.IsValidate;
            var user =UserExists(lesterModel.UserName);
            if(user != null)
            {
                result.MessageCode = MessgeCodeRegistration.ExistName;
                return result;
            }
            //if (!CommonHelper.IsValidEmail(lesterModel.Email))
            //{
            //    result.MessageCode = MessgeCodeRegistration.AccountEmailWrong;
            //    return result;
            //}
            var userEmail =_userService.GetUserByEmail(lesterModel.Email);
            if(userEmail != null)
            {
                result.MessageCode = MessgeCodeRegistration.ExistEmail;
                return result;
            }
            if (string.IsNullOrWhiteSpace(lesterModel.Password))
            {
                result.MessageCode = MessgeCodeRegistration.PasswordWrong;
                return result;
            }
            return result;
        }
        LoginResutls IsValiLogin(string userName) 
        {
            LoginResutls result =new LoginResutls();
            result.MessageCode = MessgeCodeRegistration.IsValidate;
            var user =UserExists(userName);
            if(user == null)
            {
                result.MessageCode = MessgeCodeRegistration.NotFoundName;
                return result;
            }
            if(user.LockoutEnabled)
            {
                result.MessageCode = MessgeCodeRegistration.IsLockout;
                return result;
            }
            if(user.Deleted == (int)EnumStatusUser.Delete)
            {
                result.MessageCode = MessgeCodeRegistration.IsDeleted;
                return result;
            }
            result.User = user;
            return result;
        }
        Auth_User CreateUserForLester(RegistrationLesterRequest lesterModel,string passwordHash)
        {
            var userNew = new Auth_User()
            {
                CreatedTime =DateTime.Now,
                PasswordHash = passwordHash,
                Email =lesterModel.Email,
                PhoneNumber = lesterModel.PhoneNumber,
                Status = (int)EnumStatusUser.Approved,
            };
            if (string.IsNullOrEmpty(lesterModel.Email))
            {
                userNew.UserName  = lesterModel.PhoneNumber;
            }
            else
            {
                 userNew.UserName  = lesterModel.Email.Split('@').First();;
            }
           
            return _userService.InsertUserLester(userNew);
        }
        CustomPrincipal GetInforAuthorize(Auth_User user)
        {
            CustomPrincipal customPrincipal = new CustomPrincipal()
            {
                UserId = user.Id,
                Avatar = user.Avatar,
                FullName = user.UserName,
                Roles = _rolesUserServices.GetNameRoles(user.Id)?.ToArray(),
                Permissions = _userService.GetAllPermissonOfUser(user)?.Select(x=>x.Permission)?.ToArray(),
            };

            return customPrincipal;
        }
        (string salt,string passwordHash) CreatePassswordHash(string password,string salt="")
        {
            string _passwordHash;
             var saltKey = salt;
            if(salt == "")
                saltKey = _encryptionService.CreateSaltKey(MotelUserServicesDefaults.PasswordSaltKeySize);
            _passwordHash = _encryptionService.CreatePasswordHash(password, saltKey, MotelUserServicesDefaults.DefaultHashedPasswordFormat);
            return (saltKey,_passwordHash);
        }
        LoginResutls LoginFacebook(Lesters lesters)
        {
            LoginResutls resutls = new LoginResutls();
            var user = _userService.GetUserById(lesters.UserId);
            if(user == null)
            {
                resutls.MessageCode = MessgeCodeRegistration.Error;
                return resutls;
            }
            resutls.User = user;
            resutls.customPrincipal = GetInforAuthorize(user);
            resutls.MessageCode = MessgeCodeRegistration.Suscess;
            return resutls;
        }
        
        LoginResutls registrationFacebook(RequestLoginFacebook loginFacebook)
        {
            string randomPass = CommonHelper.GenerateRandomPassword(10);
            RegistrationLesterRequest registrationLesterRequest = new RegistrationLesterRequest()
            {
                FacebookId = loginFacebook.facebookId,
                Password = randomPass,
                Email = loginFacebook.email,
                FullName = loginFacebook.name,
                PhoneNumber = loginFacebook.phone,
                UserName = loginFacebook.name
            };
            var resultRegistration = Registration(registrationLesterRequest);
            int userId = resultRegistration.Lesters == null ? 0:resultRegistration.Lesters.UserId;
            if(userId == 0)
            {
                return new LoginResutls()
                {
                    customPrincipal = resultRegistration.customPrincipal,
                    MessageCode = resultRegistration.MessageCode,
                };
            }
            var user = _userService.GetUserById(userId);
            LoginResutls loginResutls = new LoginResutls()
            {
                customPrincipal = resultRegistration.customPrincipal,
                MessageCode = resultRegistration.MessageCode,
                User = user
            };
            return loginResutls;
        }
        #endregion

        #region Methods
        public void LockOut(int userId)
        {
            throw new NotImplementedException();
        }

        public LoginResutls Login(string userName, string password)
        {
            userName= userName?.Trim();
            password= password?.Trim();
            LoginResutls loginResutls = new LoginResutls();
            var valResutls = IsValiLogin(userName);
            if(valResutls.MessageCode != MessgeCodeRegistration.IsValidate)
                return valResutls;
            var lester = _lestersRepository.Table.FirstOrDefault(x=>x.UserId == valResutls.User.Id);
            (string Salt, string passwordHash) = CreatePassswordHash(password,lester.Salt);
            if(valResutls.User.PasswordHash != passwordHash)
            {
                valResutls.MessageCode = MessgeCodeRegistration.PasswordWrong;
            }
            loginResutls.customPrincipal =  GetInforAuthorize(valResutls.User);
            loginResutls.User = valResutls.User;
            loginResutls.MessageCode = MessgeCodeRegistration.Suscess;
            return loginResutls;
        }

        public RegistrationLeterReults Registration(RegistrationLesterRequest lesterModel)
        {
            RegistrationLeterReults result =new RegistrationLeterReults();
            if (lesterModel == null)
                throw new ArgumentNullException(nameof(lesterModel));
            var resultValidate = IsValiDateRegistration(lesterModel);
            if(resultValidate.MessageCode != MessgeCodeRegistration.IsValidate)
                return resultValidate;
            var lester = new Lesters()
            { 
                FacebookId = lesterModel.FacebookId,
                FullName = lesterModel.FullName,
                IdentityCard = lesterModel.IdentityCard,
                IdentityDay  = lesterModel.IdentityDay,
                AccountName = lesterModel.UserName,
                Birthday = lesterModel.Birthday,
            };
            var saltKey = _encryptionService.CreateSaltKey(MotelUserServicesDefaults.PasswordSaltKeySize);
            (lester.Salt,lester.Password) = CreatePassswordHash(lester.Password);
            var resultUser = CreateUserForLester(lesterModel,lester.Password);
            if(resultUser == null)
            {
                result.MessageCode = MessgeCodeRegistration.Error;
                return result;
            }
            lester.UserId = resultUser.Id;
            _lestersRepository.Insert(lester);
            result.Lesters = lester;
            result.customPrincipal = GetInforAuthorize(resultUser);
            result.MessageCode = MessgeCodeRegistration.Suscess;
            return result;
        }

        public void ResetAccount(int userId)
        {
            throw new NotImplementedException();
        }

        public Auth_User UserExists(string userName)
        {
            return  _userService.GetUserByUsername(userName);
        }

        public LoginResutls RegistrationFacebook(RequestLoginFacebook loginFacebook)
        {
            LoginResutls loginResutls = new LoginResutls();
            if (loginFacebook == null)
                throw new ArgumentNullException(nameof(loginFacebook));
            var lester = GetFacebookId(loginFacebook.facebookId);
            if(lester != null)
            {
                loginResutls = LoginFacebook(lester);
            }
            else
            {
                loginResutls = registrationFacebook(loginFacebook);
            }
            return loginResutls;
        }

        public Lesters GetFacebookId(string facebookId)
        {
            try
            {
                var lester = _lestersRepository.Table.FirstOrDefault(x=>x.FacebookId == facebookId);
                return lester;
            }
            catch (Exception ex)
            {

                _logger.Error("GetFacebookId error",ex);
                return null;
            }
           
        }
        #endregion
    }
}
