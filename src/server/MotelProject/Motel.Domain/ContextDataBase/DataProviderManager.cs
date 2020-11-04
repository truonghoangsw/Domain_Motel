using Motel.Core;
using Motel.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.ContextDataBase
{
    public class DataProviderManager : IDataProviderManager
    {
        public IMotelDataProvider DataProvider
        {
            get
            {
                var dataProviderType =DataProviderType.SqlServer;

                return GetDataProvider(dataProviderType);
            }
        }

        public static IMotelDataProvider GetDataProvider(DataProviderType dataProviderType)
        {
            switch (dataProviderType)
            {
                case DataProviderType.SqlServer:
                    return new MsSqlNopDataProvider();
               
                default:
                    throw new MotelException($"Not supported data provider name: '{dataProviderType}'");
            }
        }
    }
}
