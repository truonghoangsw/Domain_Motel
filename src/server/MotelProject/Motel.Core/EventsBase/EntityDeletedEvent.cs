using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core.EventsBase
{
    public class EntityDeletedEvent<T> where T: BaseEntity
    {
        public EntityDeletedEvent(T entity)
        {
            Entity = entity;
        }
        public T Entity { get; set; }
    }
}
