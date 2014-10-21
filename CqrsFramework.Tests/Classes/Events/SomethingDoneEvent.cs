using System;
using CqrsFramework.Messaging.Events;

namespace CqrsFramework.Tests.Classes
{
    public class SomethingDoneEvent : Event
    {
        public SomethingDoneEvent(Guid aggregateId)
        {
            Id = aggregateId;
        }
    }
}