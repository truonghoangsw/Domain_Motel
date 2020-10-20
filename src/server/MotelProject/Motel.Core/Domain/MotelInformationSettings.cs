using Motel.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core.Domain
{
    public class MotelInformationSettings: ISettings
    {
        public bool DisplayMiniProfilerInPublicStore { get; set; }

        public bool DisplayMiniProfilerInPublicMotel { get; set; }

    }
}
