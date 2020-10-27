using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core.EventsBase
{
    public class EntityInsertedEvent<T> where T: BaseEntity
    {
        public T Entity { get; set; }
        public EntityInsertedEvent(T entity)
        {
            Entity = entity;
        }
    }
}
