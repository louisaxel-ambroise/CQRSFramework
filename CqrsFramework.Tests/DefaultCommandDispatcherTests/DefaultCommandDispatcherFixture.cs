using CqrsFramework.Messaging.Commands;

namespace CqrsFramework.Tests.DefaultCommandDispatcherTests
{
    public abstract class DefaultCommandDispatcherFixture : BaseTestFixture
    {
        protected ICommandDispatcher CommandDispatcher { get; set; }
        protected ICommandHandler[] Handlers { get; set; }

        protected override void Given()
        {
            CommandDispatcher = new DefaultCommandDispatcher(Handlers);
        }
    }
}