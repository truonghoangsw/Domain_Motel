using Motel.Domain;
using Motel.Services.Common;
using Motel.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Helper
{
    public partial class DateTimeHelper:IDateTimeHelper
    {
         #region Fields

        private readonly DateTimeSettings _dateTimeSettings;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ISettingService _settingService;
        private readonly IWorkContext _workContext;

        #endregion
    }
}
