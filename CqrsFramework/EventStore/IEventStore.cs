using System;
using System.Collections.Generic;
using CqrsFramework.Messaging.Events;

namespace CqrsFramework.EventStore
{
    public interface IEventStore
    {
        IEnumerable<Event> LoadForAggregate(Guid aggregateId);
        void Save<T>(Guid aggregateId, T @event) where T : Event;
    }
}