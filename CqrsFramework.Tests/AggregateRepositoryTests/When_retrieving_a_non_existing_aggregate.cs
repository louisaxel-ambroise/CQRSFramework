using System;
using CqrsFramework.Aggregate;
using CqrsFramework.Exceptions;
using NUnit.Framework;

namespace CqrsFramework.Tests.AggregateRepositoryTests
{
    [TestFixture]
    public class When_retrieving_a_non_existing_aggregate : AggregateRepositoryFixture
    {
        protected Guid AggregateId { get; set; }
        protected AggregateRoot Aggregate { get; set; }
        protected Exception Catched { get; set; }

        protected override void Given()
        {
            base.Given();

            AggregateId = Guid.NewGuid();

            EventStoreMock
                .Setup(x => x.LoadForAggregate(AggregateId))
                .Throws(new AggregateNotFoundException(AggregateId));
        }

        protected override void When()
        {
            try
            {
                Aggregate = Repository.GetById(AggregateId);
            }
            catch (Exception ex)
            {
                Catched = ex;
            }
        }

        [Then]
        public void It_should_throws_an_exception()
        {
            Assert.IsNotNull(Catched);
        }

        [Then]
        public void It_should_throw_an_AggregateNotFoundException()
        {
            Assert.IsInstanceOf<AggregateNotFoundException>(Catched);
        }

        [Then]
        public void It_should_throw_an_exception_for_the_requested_aggregate_id()
        {
            var aggregateNotFound = Catched as AggregateNotFoundException;

            Assert.AreEqual(AggregateId, aggregateNotFound.AggregateId);
        }

        [Then]
        public void It_should_not_return_an_aggregate()
        {
            Assert.IsNull(Aggregate);
        }
    }
}