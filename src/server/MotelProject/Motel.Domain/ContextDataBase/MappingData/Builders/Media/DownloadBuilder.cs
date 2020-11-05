using FluentMigrator.Builders.Create.Table;
using Motel.Domain.Domain.Media;

namespace  Motel.Domain.ContextDataBase.MappingData.Builders.Media
{
    /// <summary>
    /// Represents a download entity builder
    /// </summary>
    public partial class DownloadBuilder : MotelEntityBuilde<Download>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
        }

        #endregion
    }
}