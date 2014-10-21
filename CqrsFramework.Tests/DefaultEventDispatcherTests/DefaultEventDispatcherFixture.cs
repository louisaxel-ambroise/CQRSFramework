using CqrsFramework.Messaging.Events;

namespace CqrsFramework.Tests.DefaultEventDispatcherTests
{
    public abstract class DefaultEventDispatcherFixture : BaseTestFixture
    {
        protected DefaultEventDispatcher EventDispatcher { get; set; }
        protected IEventHandler[] Handlers { get; set; }

        protected override void Given()
        {
            EventDispatcher = new DefaultEventDispatcher(Handlers);
        }
    }
}