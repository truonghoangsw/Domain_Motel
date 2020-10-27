using Motel.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core
{
    public class CommonSettings: ISettings
    {
         public List<string> IgnoreLogWordlist { get; set; }
    }
}
