using CqrsFramework.EventStore;
using CqrsFramework.Repository;
using CqrsFramework.Tests.Classes;
using Moq;

namespace CqrsFramework.Tests.AggregateRepositoryTests
{
    public abstract class AggregateRepositoryFixture : BaseTestFixture
    {
        protected IRepository<TestAggregate> Repository { get; set; }
        protected Mock<IEventStore> EventStoreMock { get; set; }

        protected override void Given()
        {
            EventStoreMock = new Mock<IEventStore>();

            Repository = new SimpleRepository<TestAggregate>(EventStoreMock.Object);
        }
    }
}