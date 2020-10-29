using Motel.Core.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Security
{
    public class MotelUserServicesDefaults
    {
        public static string CustomerCustomerRolesPrefixCacheKey => "Motel.customer.customerrole";

        public static CacheKey UserRoleIdsCacheKey => new CacheKey("Motel.customer.customerrole.ids-{0}-{1}", CustomerCustomerRolesPrefixCacheKey);

    }
}
