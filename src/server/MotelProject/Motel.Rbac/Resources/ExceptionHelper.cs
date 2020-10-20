using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Motel.Rbac.Resources
{
    public class ExceptionHelper
    {
        internal static class ExceptionHelper
        {
            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Method may not be used in every assembly it is imported into")]
            internal static ArgumentException CreateArgumentNullOrEmptyException(string paramName)
            {
                return new ArgumentException(SecurityResource.fileterContext, paramName);
            }
        }
    }
}
