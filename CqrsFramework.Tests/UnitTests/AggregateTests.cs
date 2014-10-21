using CqrsFramework.Tests.Classes;
using NUnit.Framework;

namespace CqrsFramework.Tests.UnitTests
{
    [TestFixture]
    public class AggregateTests
    {
        [Test]
        public void Calling_Apply_without_handler_should_not_apply_event()
        {
            var aggregate = new TestAggregate();
            aggregate.Apply(new NoHandlerEvent());

            Assert.IsEmpty(aggregate.GetPendingEvents());
        }

        [Test]
        public void Calling_Apply_with_handler_should_not_apply_event()
        {
            var aggregate = new TestAggregate();
            aggregate.Apply(new AggregateEvent());

            Assert.IsNotEmpty(aggregate.GetPendingEvents());
        }
    }
}