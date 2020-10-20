using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core.Configuration
{
    public partial class MotelConfig
    {
        public bool RedisEnabled { get; set; }
        public string RedisConnectionString { get; set; }
        public int? RedisDatabaseId { get; set; }
        public bool UseRedisToStoreDataProtectionKeys { get; set; }
        public bool UseRedisForCaching { get; set; }
        public bool IgnoreRedisTimeoutException { get; set; }
        public bool UseRedisToStorePluginsInfo { get; set; }
    }
}
