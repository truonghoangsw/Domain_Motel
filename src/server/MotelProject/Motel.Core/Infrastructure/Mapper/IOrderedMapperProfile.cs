using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core.Infrastructure.Mapper
{
    public interface IOrderedMapperProfile
    {
         int Order { get; }
    }
}
