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
        private readonly IPermissionService permissionServices;
        private readonly IWorkContext _workContext;
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
            this.permissionServices = permissionServices;
            _workContext = workContext;
            _encryptionService = encryptionService;
            _eventPublisher = eventPublisher;
        }
        #endregion

        #region Utilities
        RegistrationLeterReults IsValiDate(RegistrationLesterRequest lesterModel)
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
        #endregion

        #region Methods
        public void LockOut(int userId)
        {
            throw new NotImplementedException();
        }

        public LoginResutls Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public RegistrationLeterReults Registration(RegistrationLesterRequest lesterModel)
        {
            RegistrationLeterReults result =new RegistrationLeterReults();
            if (lesterModel == null)
                throw new ArgumentNullException(nameof(lesterModel));
            var resultValidate = IsValiDate(lesterModel);
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
            lester.Salt = saltKey;
            lester.Password = _encryptionService.CreatePasswordHash(lesterModel.Password, saltKey, MotelUserServicesDefaults.DefaultHashedPasswordFormat);
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
