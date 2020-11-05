using FluentMigrator.Runner;
using FluentMigrator.Runner.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.ContextDataBase.Migrations
{
    public class MotelConventionSet : IConventionSet
    {
        #region Ctor
         public MotelConventionSet(IMotelDataProvider dataProvider)
            : this(new DefaultConventionSet(), new MotelForeignKeyConvention(dataProvider), new MotelIndexConvention(dataProvider))
        {
        }        
        public MotelConventionSet(IConventionSet innerConventionSet, MotelForeignKeyConvention foreignKeyConvention, MotelIndexConvention indexConvention)
        {
            ForeignKeyConventions = new List<IForeignKeyConvention>
            {
                foreignKeyConvention,
                innerConventionSet.SchemaConvention,
            };

            IndexConventions = new List<IIndexConvention>
            {
                indexConvention
            };
            
            ColumnsConventions = innerConventionSet.ColumnsConventions;
            ConstraintConventions = innerConventionSet.ConstraintConventions;
            
            SequenceConventions = innerConventionSet.SequenceConventions;
            AutoNameConventions = innerConventionSet.AutoNameConventions;
            SchemaConvention = innerConventionSet.SchemaConvention;
            RootPathConvention = innerConventionSet.RootPathConvention;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the root path convention to be applied to <see cref="T:FluentMigrator.Expressions.IFileSystemExpression" /> implementations
        /// </summary>
        public IRootPathConvention RootPathConvention { get; }

        /// <summary>
        /// Gets the default schema name convention to be applied to <see cref="T:FluentMigrator.Expressions.ISchemaExpression" /> implementations
        /// </summary>
        /// <remarks>
        /// This class cannot be overridden. The <see cref="T:FluentMigrator.Runner.Conventions.IDefaultSchemaNameConvention" />
        /// must be implemented/provided instead.
        /// </remarks>
        public DefaultSchemaConvention SchemaConvention { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IColumnsExpression" /> implementations
        /// </summary>
        public IList<IColumnsConvention> ColumnsConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IConstraintExpression" /> implementations
        /// </summary>
        public IList<IConstraintConvention> ConstraintConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IForeignKeyExpression" /> implementations
        /// </summary>
        public IList<IForeignKeyConvention> ForeignKeyConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IIndexExpression" /> implementations
        /// </summary>
        public IList<IIndexConvention> IndexConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.ISequenceExpression" /> implementations
        /// </summary>
        public IList<ISequenceConvention> SequenceConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IAutoNameExpression" /> implementations
        /// </summary>
        public IList<IAutoNameConvention> AutoNameConventions { get; }

        #endregion
    }
}
