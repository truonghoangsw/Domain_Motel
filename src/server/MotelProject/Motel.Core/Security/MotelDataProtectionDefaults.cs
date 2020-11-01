using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core.Security
{
    public class MotelDataProtectionDefaults
    {
          /// </summary>
        public static string RedisDataProtectionKey => "Motel.DataProtectionKeys";

        /// <summary>
        /// Gets the name of the key file used to store the protection key list to Azure (used with the UseAzureBlobStorageToStoreDataProtectionKeys option enabled)
        /// </summary>
        public static string AzureDataProtectionKeyFile => "DataProtectionKeys.xml";

        /// <summary>
        /// Gets the name of the key path used to store the protection key list to local file system (used when UseAzureBlobStorageToStoreDataProtectionKeys and PersistDataProtectionKeysToRedis options not enabled)
        /// </summary>
        public static string DataProtectionKeysPath => "~/App_Data/DataProtectionKeys";
    }
}
