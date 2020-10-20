using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core.Caching
{
    public class MotelCachingDefaults
    {
        public static int CacheTime => 60;
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : Entity type name
        /// {1} : Entity id
        /// </remarks>
        public static string MotelEntityCacheKey => "Motel.{0}.id-{1}";
    }
}
