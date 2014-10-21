using System;
using CqrsFramework.Messaging.Commands;
using CqrsFramework.Tests.Classes.Commands;
using Moq;
using NUnit.Framework;

namespace CqrsFramework.Tests.DefaultCommandDispatcherTests
{
    public class When_dispatching_a_command_with_multiple_registered_handlers : DefaultCommandDispatcherFixture
    {
        protected Exception Catched { get; set; }
        protected Mock<ICommandHandler<CreateAggregateCommand>> CreateAggregateHandlerMock { get; set; }

        protected override void Given()
        {
            CreateAggregateHandlerMock = new Mock<ICommandHandler<CreateAggregateCommand>>();
            Handlers = new ICommandHandler[] {CreateAggregateHandlerMock.Object, CreateAggregateHandlerMock.Object};

            base.Given();
        }

        protected override void When()
        {
            try
            {
                CommandDispatcher.Dispatch(new CreateAggregateCommand(Guid.NewGuid()));
            }
            catch (Exception ex)
            {
                Catched = ex;
            }
        }

        [Then]
        public void It_has_thrown_an_exception()
        {
            Assert.IsNotNull(Catched);
        }

        [Then]
        public void It_has_thrown_an_ArgumentException()
        {
            Assert.IsInstanceOf<ArgumentException>(Catched);
        }

        [Then]
        public void It_has_thrown_an_exception_indicating_only_one_command_handler_is_accepted()
        {
            Assert.AreEqual("Unable to dispatch command to more than one handler.", Catched.Message);
        }
    }
}