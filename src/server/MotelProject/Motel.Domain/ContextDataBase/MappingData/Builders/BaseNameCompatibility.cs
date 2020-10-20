using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.ContextDataBase.MappingData.Builders
{
    public class BaseNameCompatibility : INameCompatibility
    {
        public Dictionary<Type, string> TableNames =>  new Dictionary<Type, string> { };

        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string> { };
    }
}
