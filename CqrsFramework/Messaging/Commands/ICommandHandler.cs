namespace CqrsFramework.Messaging.Commands
{
    public interface ICommandHandler { }

    public interface ICommandHandler<in T> : ICommandHandler where T : Command
    {
        void Handle(T command);
    }
}