using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.ContextDataBase
{
    public class MotelDataSettingsDefaults
    {
        public static string ObsoleteFilePath => "~/App_Data/Settings.txt";
        public static string FilePath => "~/App_Data/dataSettings.json";

    }
}
