using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Events
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event);
    }
}
