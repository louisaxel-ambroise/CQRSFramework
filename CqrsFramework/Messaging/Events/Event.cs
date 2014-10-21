using System;

namespace CqrsFramework.Messaging.Events
{
    public abstract class Event : IMessage
    {
        public Guid Id { get; protected set; }
        public int Version { get; protected set; }

        public readonly DateTime RecordTime = DateTime.Now;
    }
}