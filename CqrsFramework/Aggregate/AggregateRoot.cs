using System;
using System.Collections.Generic;
using System.Linq;
using CqrsFramework.Messaging.Events;
using NLog;
using ReflectionMagic;

namespace CqrsFramework.Aggregate
{
    public abstract class AggregateRoot
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly IList<Event> _pendingChanges = new List<Event>();

        public Guid Id { get; protected set; }

        public AggregateRoot() { }

        public IEnumerable<Event> GetPendingEvents()
        {
            return _pendingChanges.ToArray();
        }

        public void CommitPendingEvent(Event @event)
        {
            _pendingChanges.Remove(@event);
        }

        public void Apply(Event @event)
        {
            Apply(@event, true);
        }

        public void Apply(Event @event, bool isNew)
        {
            try
            {
                this.AsDynamic().Handle(@event);

                if (isNew)
                {
                    _pendingChanges.Add(@event);
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("No handler found for event type '{0}'", @event.GetType().Name), ex);
            }
        }
    }
}