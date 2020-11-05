using FluentMigrator.Builders.Create.Table;
using Motel.Domain.Domain.Media;
using Motel.Domain.Domain.Post;


namespace Motel.Domain.ContextDataBase.MappingData.Builders.Media
{
    /// <summary>
    /// Represents a picture binary entity builder
    /// </summary>
    public partial class PictureBinaryBuilder : MotelEntityBuilde<PictureBinary>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(PictureBinary.PictureId)).AsInt32().ForeignKey<Picture>();
        }

        #endregion
    }
}