using System;
using CqrsFramework.Messaging.Events;
using CqrsFramework.Tests.Classes;
using Moq;
using NUnit.Framework;

namespace CqrsFramework.Tests.AggregateRepositoryTests
{
    [TestFixture]
    public class When_saving_an_aggregate_with_changes : AggregateRepositoryFixture
    {
        protected Guid AggregateId { get; set; }
        protected TestAggregate Aggregate { get; set; }

        protected override void Given()
        {
            base.Given();

            AggregateId = Guid.NewGuid();
            Aggregate = new TestAggregate();
            Aggregate.Apply(new SomethingDoneEvent(AggregateId));
            Aggregate.Apply(new AggregateEvent());
        }

        protected override void When()
        {
            Repository.Save(Aggregate);
        }

        [Then]
        public void It_has_called_the_EventStore_save_method()
        {
            EventStoreMock.Verify(x => x.Save(It.IsAny<Guid>(), It.IsAny<Event>()), Times.AtLeastOnce);
        }

        [Then]
        public void It_has_called_EventStore_save_method_once_per_pending_event()
        {
            EventStoreMock.Verify(x => x.Save(It.IsAny<Guid>(), It.IsAny<Event>()), Times.Exactly(2));
        }

        [Then]
        public void It_has_commited_all_the_aggregate_pending_changes()
        {
            Assert.IsEmpty(Aggregate.GetPendingEvents());
        }
    }
}