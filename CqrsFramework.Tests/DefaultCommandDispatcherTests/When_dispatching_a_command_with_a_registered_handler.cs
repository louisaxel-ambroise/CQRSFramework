using System;
using CqrsFramework.Messaging.Commands;
using CqrsFramework.Tests.Classes.Commands;
using Moq;
using NUnit.Framework;

namespace CqrsFramework.Tests.DefaultCommandDispatcherTests
{
    [TestFixture]
    public class When_dispatching_a_command_with_a_registered_handler : DefaultCommandDispatcherFixture
    {
        protected Guid CommandId { get; set; }
        protected Mock<ICommandHandler<CreateAggregateCommand>> CreateAggregateHandlerMock { get; set; }

        protected override void Given()
        {
            CreateAggregateHandlerMock = new Mock<ICommandHandler<CreateAggregateCommand>>();
            Handlers = new ICommandHandler[] { CreateAggregateHandlerMock.Object };

            base.Given();
        }

        protected override void When()
        {
            CommandDispatcher.Dispatch(new CreateAggregateCommand(CommandId));
        }

        [Then]
        public void It_has_called_the_command_handler_Handle_method()
        {
            CreateAggregateHandlerMock.Verify(x => x.Handle(It.IsAny<CreateAggregateCommand>()), Times.Once);
        }

        [Then]
        public void It_has_called_the_command_handler_Handle_method_with_correct_command()
        {
            CreateAggregateHandlerMock.Verify(x => x.Handle(It.Is<CreateAggregateCommand>(c => c.Id == CommandId)), Times.Once);
        }
    }
}