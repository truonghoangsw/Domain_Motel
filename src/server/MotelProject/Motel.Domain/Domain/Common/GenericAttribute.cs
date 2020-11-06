﻿using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Common
{
    public partial class GenericAttribute : BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int EntityId { get; set; }
        
        /// <summary>
        /// Gets or sets the key group
        /// </summary>
        public string KeyGroup { get; set; }

        /// <summary>
        /// Gets or sets the key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public string Value { get; set; }

        /// </summary>
        public DateTime? CreatedOrUpdatedDateUTC { get; set; }
    }
}
