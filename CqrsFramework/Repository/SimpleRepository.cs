using System;
using CqrsFramework.Aggregate;
using CqrsFramework.EventStore;

namespace CqrsFramework.Repository
{
    public sealed class SimpleRepository<TAggregate> : IRepository<TAggregate> where TAggregate : AggregateRoot, new()
    {
        private readonly IEventStore _eventStore;

        public SimpleRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public TAggregate GetById(Guid id)
        {
            var aggregateHistory = _eventStore.LoadForAggregate(id);
            var aggregate = new TAggregate();

            foreach (var @event in aggregateHistory)
            {
                aggregate.Apply(@event, false);
            }

            return aggregate;
        }

        public void Save(TAggregate aggregate)
        {
            foreach (var pendingEvent in aggregate.GetPendingEvents())
            {
                _eventStore.Save(aggregate.Id, pendingEvent);
                aggregate.CommitPendingEvent(pendingEvent);
            }
        }
    }
}