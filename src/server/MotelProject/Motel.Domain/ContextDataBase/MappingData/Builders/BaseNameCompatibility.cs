using Motel.Domain.Domain.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.ContextDataBase.MappingData.Builders
{
    public class BaseNameCompatibility : INameCompatibility
    {
        public Dictionary<Type, string> TableNames =>  new Dictionary<Type, string> 
        { 
             { typeof(PostPictureMaping), "PostRental_Picture_Mapping" },
            { typeof(PostCategoryMapping), "RentalPost_Category_Mapping" }
        };

        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string> { };
    }
}
