using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core.Caching
{
    public interface ILocker
    {
          bool PerformActionWithLock(string resource, TimeSpan expirationTime, Action action);
    }
}
