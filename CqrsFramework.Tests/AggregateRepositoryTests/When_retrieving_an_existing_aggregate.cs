using System;
using CqrsFramework.Aggregate;
using CqrsFramework.Tests.Classes;
using Moq;
using NUnit.Framework;

namespace CqrsFramework.Tests.AggregateRepositoryTests
{
    [TestFixture]
    public class When_retrieving_an_existing_aggregate : AggregateRepositoryFixture
    {
        protected Guid AggregateId { get; set; }
        protected AggregateRoot Aggregate { get; set; }

        protected override void Given()
        {
            base.Given();

            AggregateId = Guid.NewGuid();

            EventStoreMock
                .Setup(x => x.LoadForAggregate(AggregateId))
                .Returns(new []{ new SomethingDoneEvent(AggregateId) });
        }

        protected override void When()
        {
            Aggregate = Repository.GetById(AggregateId);
        }

        [Then]
        public void It_should_return_an_aggregate()
        {
            Assert.IsNotNull(Aggregate);
        }

        [Then]
        public void It_should_return_an_aggregate_of_the_correct_type()
        {
            Assert.IsInstanceOf<TestAggregate>(Aggregate);
        }

        [Then]
        public void It_should_have_called_the_EventStore_LoadForAggregate_method()
        {
            EventStoreMock.Verify(x => x.LoadForAggregate(It.IsAny<Guid>()), Times.Once);
        }

        [Then]
        public void It_should_have_called_the_EventStore_LoadForAggregate_method_with_the_requested_aggregate_id()
        {
            EventStoreMock.Verify(x => x.LoadForAggregate(AggregateId), Times.Once);
        }

        [Then]
        public void It_should_have_applied_all_events_to_the_aggregate()
        {
            var testAggregate = Aggregate as TestAggregate;

            Assert.AreEqual(1, testAggregate.AppliedEvents);
        }
    }
}