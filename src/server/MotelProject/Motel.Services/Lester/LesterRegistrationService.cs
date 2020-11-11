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
    using System.Linq;

    public class LesterRegistrationService : ILesterRegistrationService
    {

        #region Fields
        private readonly IUserService _userService;
        private readonly IRolesUserServices _rolesUserServices;
        private readonly IPermissionService _permissionServices;
        private readonly IEncryptionService _encryptionService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<Lesters> _lestersRepository;
        #endregion

        #region Ctor
        public LesterRegistrationService(
            IUserService userService, 
            IRolesUserServices rolesUserServices, 
            IPermissionService permissionServices, 
            IWorkContext workContext, 
            IEncryptionService encryptionService, 
            IEventPublisher eventPublisher)
        {
            _userService = userService;
            _rolesUserServices = rolesUserServices;
            _permissionServices = permissionServices;
            _encryptionService = encryptionService;
            _eventPublisher = eventPublisher;
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
            if (!CommonHelper.IsValidEmail(lesterModel.Email))
            {
                result.MessageCode = MessgeCodeRegistration.AccountEmailWrong;
                return result;
            }
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
            return result;
        }
        Auth_User CreateUserForLester(RegistrationLesterRequest lesterModel,string passwordHash)
        {
            var userNew = new Auth_User()
            {
                UserName = lesterModel.UserName,
                CreatedTime =DateTime.Now,
                PasswordHash = passwordHash,
                Email =lesterModel.Email,
                PhoneNumber = lesterModel.PhoneNumber,
                Status = (int)EnumStatusUser.Approved,
            };
            return _userService.InsertUserLester(userNew);
        }
        CustomPrincipal GetInforAuthorize(Auth_User user)
        {
            CustomPrincipal customPrincipal = new CustomPrincipal()
            {
                Avatar = user.Avatar,
                FullName = user.UserName,
                Roles = _rolesUserServices.GetNameRoles(user.Id)?.ToArray(),
                Permissions = _userService.GetAllPermissonOfUser(user)?.Select(x=>x.Permission)?.ToArray(),
            };

            return customPrincipal;
        }
        (string salt,string passwordHash) CreatePassswordHash(string password)
        {
            string _passwordHash;
            var saltKey = _encryptionService.CreateSaltKey(MotelUserServicesDefaults.PasswordSaltKeySize);
            string _salt = saltKey;
            _passwordHash = _encryptionService.CreatePasswordHash(password, saltKey, MotelUserServicesDefaults.DefaultHashedPasswordFormat);
            return (_salt,_passwordHash);
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
            (string salt, string passwordHash) = CreatePassswordHash(password);
            if(valResutls.User.PasswordHash != password)
            {
                valResutls.MessageCode = MessgeCodeRegistration.PasswordWrong;
            }
            loginResutls.customPrincipal =  GetInforAuthorize(valResutls.User);
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

       

        #endregion
    }
}
