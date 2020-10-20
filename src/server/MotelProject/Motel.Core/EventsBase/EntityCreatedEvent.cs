using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core.EventsBase
{
    public class EntityCreatedEvent<T> where T: BaseEntity
    {
        public T Entity { get; set; }
        public EntityCreatedEvent(T entity)
        {
            Entity = entity;
        }
    }
}
