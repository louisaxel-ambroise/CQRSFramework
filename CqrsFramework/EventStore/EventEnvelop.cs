using System;

namespace CqrsFramework.EventStore
{
    public sealed class EventEnvelop
    {
        public string Type { get; internal set; }
        public Guid AggregateId { get; internal set; }
        public int Version { get; internal set; }
    }
}