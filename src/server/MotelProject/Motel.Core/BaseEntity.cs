using Motel.Core.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core
{
    public  abstract partial class BaseEntity
    {
        public int Id { get; set; }
        public string EntityCacheKey => GetEntityCacheKey(GetType(),Id);

        public static string GetEntityCacheKey(Type entityType,object Id)
        {
            return string.Format(MotelCachingDefaults.MotelEntityCacheKey,entityType.Name.ToLower(),Id);
        }
    }
}
