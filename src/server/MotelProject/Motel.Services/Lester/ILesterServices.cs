using Motel.Domain.Domain.Lester;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Lester
{
    public interface ILesterServices
    {
        #region Lester Services
        Lesters GetByUserId(int Id);
        #endregion
    }
}
