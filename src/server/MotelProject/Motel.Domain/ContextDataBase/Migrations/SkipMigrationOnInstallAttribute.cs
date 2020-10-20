using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.ContextDataBase.Migrations
{
   [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class SkipMigrationOnInstallAttribute : Attribute
    {
    }
}
