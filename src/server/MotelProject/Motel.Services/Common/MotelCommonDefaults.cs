using Motel.Core.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Common
{
    public class MotelCommonDefaults
    {
        public static CacheKey GenericAttributeCacheKey => new CacheKey("Motel.genericattribute.{0}-{1}");

    }
}
