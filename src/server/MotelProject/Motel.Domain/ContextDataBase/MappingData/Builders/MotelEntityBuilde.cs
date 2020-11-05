using FluentMigrator.Builders.Create.Table;
using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.ContextDataBase.MappingData.Builders
{
    public abstract class MotelEntityBuilde<TEntity> : IEntityBuilder where TEntity : BaseEntity
    {
         public abstract void MapEntity(CreateTableExpressionBuilder table);
    }
}
