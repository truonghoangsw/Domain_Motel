using FluentMigrator;
using Motel.Core.Infrastructure;
using Motel.Domain.ContextDataBase;
using Motel.Domain.ContextDataBase.Migrations;
using Motel.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Api.Framework
{
    [SkipMigrationOnInstall]
    public class SettingMigration: MigrationBase
    {
        /// <summary>Collect the UP migration expressions</summary>
        public override void Up()
        {
            if (!DataSettingsManager.DatabaseIsInstalled)
                return;

            //do not use DI, because it produces exception on the installation process
            var settingService = EngineContext.Current.Resolve<ISettingService>();
            //use settingService to add, update and delete settings
        }

        public override void Down()
        {
            //add the downgrade logic if necessary 
        }
    }
}
