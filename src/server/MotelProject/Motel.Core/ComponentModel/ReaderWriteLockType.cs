using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core.ComponentModel
{
    public enum ReaderWriteLockType
    {
        Read,
        Write,
        UpgradeableRead
    }
}
