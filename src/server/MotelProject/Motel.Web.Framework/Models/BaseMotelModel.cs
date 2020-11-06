using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Motel.Web.Framework.Models
{
    public partial class BaseMotelModel
    {
        #region Ctor
        public BaseMotelModel()
        {
            CustomProperties = new Dictionary<string, object>();
            PostInitialize();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Perform additional actions for binding the model
        /// </summary>
        /// <param name="bindingContext">Model binding context</param>
        /// <remarks>Developers can override this method in custom partial classes in order to add some custom model binding</remarks>
        public virtual void BindModel(ModelBindingContext bindingContext)
        {
        }

        /// <summary>
        /// Perform additional actions for the model initialization
        /// </summary>
        /// <remarks>Developers can override this method in custom partial classes in order to add some custom initialization code to constructors</remarks>
        
        protected virtual void PostInitialize()
        {
        }
        #endregion

        #region Properties
        [XmlIgnore]
        public Dictionary<string, object> CustomProperties { get; set; }
        #endregion
    }
}
