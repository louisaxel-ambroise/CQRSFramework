using System;
using CqrsFramework.Exceptions;
using CqrsFramework.Messaging.Events;
using CqrsFramework.Tests.Classes;
using Moq;
using NUnit.Framework;

namespace CqrsFramework.Tests.AggregateRepositoryTests
{
    [TestFixture]
    public class When_saving_an_aggregate_with_wrong_event_version : AggregateRepositoryFixture
    {
        protected Guid AggregateId { get; set; }
        protected TestAggregate Aggregate { get; set; }
        protected Exception Catched { get; set; }

        protected override void Given()
        {
            base.Given();

            AggregateId = Guid.NewGuid();
            EventStoreMock.Setup(x => x.Save(AggregateId, It.IsAny<Event>())).Throws(new AggregateConcurrencyException(AggregateId, 2, 3));

            Aggregate = new TestAggregate();
            Aggregate.Apply(new SomethingDoneEvent(AggregateId), true);
        }

        protected override void When()
        {
            try
            {
                Repository.Save(Aggregate);
            }
            catch (Exception ex)
            {
                Catched = ex;
            }
        }

        [Then]
        public void It_should_throw_an_exception()
        {
            Assert.IsNotNull(Catched);
        }

        [Test]
        public void It_should_throw_an_AggregateConcurrencyException()
        {
            Assert.IsInstanceOf<AggregateConcurrencyException>(Catched);
        }

        [Then]
        public void It_has_not_commited_all_the_aggregate_pending_changes()
        {
            Assert.IsNotEmpty(Aggregate.GetPendingEvents());
        }
    }
}