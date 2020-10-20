using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.ContextDataBase
{
     /// <summary>
    /// Represents a data provider manager
    /// </summary>
    public partial interface IDataProviderManager
    {
        #region Properties

        /// <summary>
        /// Gets data provider
        /// </summary>
        IMotelDataProvider DataProvider { get; }

        #endregion
    }
}
