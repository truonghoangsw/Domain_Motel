using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Web.Framework.Models
{
    public abstract partial class BasePagedListModel<T> : BaseMotelModel, IPagedModel<T> where T : BaseMotelModel
    {
        /// <summary>
        /// Gets or sets data records
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Gets or sets draw
        /// </summary>
        public string Draw { get; set; }

        /// <summary>
        /// Gets or sets a number of filtered data records
        /// </summary>
        public int RecordsFiltered { get; set; }

        /// <summary>
        /// Gets or sets a number of total data records
        /// </summary>
        public int RecordsTotal { get; set; }        
    }
}
