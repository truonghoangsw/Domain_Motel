using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core.EventsBase
{
    public class EntityUpdatedEvent<T> where T: BaseEntity
    {
        public T Entity { get; set; }
        public EntityUpdatedEvent(T entity)
        {
            Entity = entity;
        }
    }
}
