using Motel.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core.Caching
{
    public class CachingSettings: ISettings
    {
        public int DefaultCacheTime { get; set; }
        public int ShortTermCacheTime { get; set; }
        public int BundledFilesCacheTime { get; set; }
    }
}
