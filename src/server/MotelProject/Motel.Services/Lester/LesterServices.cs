using Motel.Core.Caching;
using Motel.Domain.ContextDataBase;
using Motel.Domain.Domain.Lester;
using Motel.Services.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Lester
{
    public class LesterServices: ILesterServices
    {
        #region Fields
        private readonly CachingSettings _cachingSettings;
        private readonly LesterSettings _customerSettings;
        private readonly UserService _userService;
        private readonly IPermissionService _permissionService;
        private readonly IRepository<Lesters> _lestersRepository;

        
        #endregion

        #region Ctor

        #endregion


        #region Utilities

        #endregion


        #region Method
        public Lesters InsertLesters(Lesters lesters)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
