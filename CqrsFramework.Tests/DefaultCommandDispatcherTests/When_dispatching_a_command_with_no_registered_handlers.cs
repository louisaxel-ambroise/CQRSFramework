using System;
using CqrsFramework.Messaging.Commands;
using CqrsFramework.Tests.Classes.Commands;
using NUnit.Framework;

namespace CqrsFramework.Tests.DefaultCommandDispatcherTests
{
    [TestFixture]
    public class When_dispatching_a_command_with_no_registered_handlers : DefaultCommandDispatcherFixture
    {
        protected override void Given()
        {
            Handlers = new ICommandHandler[0];
            base.Given();
        }

        protected override void When()
        {
            CommandDispatcher.Dispatch(new CreateAggregateCommand(Guid.NewGuid()));
        }

        [Then]
        public void It_returns_silently()
        {
            // Just ensure no exceptions are thrown.
        }
    }
}