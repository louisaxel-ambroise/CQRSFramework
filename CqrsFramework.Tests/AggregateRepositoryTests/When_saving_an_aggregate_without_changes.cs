using System;
using CqrsFramework.Messaging.Events;
using CqrsFramework.Tests.Classes;
using Moq;
using NUnit.Framework;

namespace CqrsFramework.Tests.AggregateRepositoryTests
{
    [TestFixture]
    public class When_saving_an_aggregate_without_changes : AggregateRepositoryFixture
    {
        protected TestAggregate Aggregate { get; set; }

        protected override void Given()
        {
            base.Given();

            Aggregate = new TestAggregate();
        }

        protected override void When()
        {
            Repository.Save(Aggregate);
        }

        [Then]
        public void It_has_not_called_the_EventStore_save_method()
        {
            EventStoreMock.Verify(x => x.Save(It.IsAny<Guid>(), It.IsAny<Event>()), Times.Never);
        }

        [Then]
        public void It_has_not_changes_the_aggregate_pending_events()
        {
            Assert.IsEmpty(Aggregate.GetPendingEvents());
        }
    }
}