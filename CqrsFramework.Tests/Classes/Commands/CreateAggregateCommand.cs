using System;
using CqrsFramework.Messaging.Commands;

namespace CqrsFramework.Tests.Classes.Commands
{
    public class CreateAggregateCommand : Command
    {
        public readonly Guid Id;

        public CreateAggregateCommand(Guid commandId)
        {
            Id = commandId;
        }
    }
}