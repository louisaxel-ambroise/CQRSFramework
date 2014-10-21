using CqrsFramework.Messaging.Events;
using CqrsFramework.Tests.Classes;
using Moq;
using NUnit.Framework;

namespace CqrsFramework.Tests.DefaultEventDispatcherTests
{
    [TestFixture]
    public class When_dispatching_event_with_a_registered_handler : DefaultEventDispatcherFixture
    {
        protected Mock<IEventHandler<AggregateEvent>> HandlerMock { get; set; }

        protected override void Given()
        {
            HandlerMock = new Mock<IEventHandler<AggregateEvent>>();
            Handlers = new IEventHandler[] {HandlerMock.Object};

            base.Given();
        }

        protected override void When()
        {
            EventDispatcher.Dispatch(new AggregateEvent());
        }

        [Then]
        public void It_has_called_the_handler_Handle_method()
        {
            HandlerMock.Verify(x => x.Handle(It.IsAny<AggregateEvent>()), Times.Once);
        }
    }
}