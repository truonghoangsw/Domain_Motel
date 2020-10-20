using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.ContextDataBase.MappingData.Builders
{
    public interface IEntityBuilder
    {
        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        void MapEntity(CreateTableExpressionBuilder table);
    }
}
