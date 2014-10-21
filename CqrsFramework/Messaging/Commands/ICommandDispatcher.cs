namespace CqrsFramework.Messaging.Commands
{
    public interface ICommandDispatcher
    {
        void Dispatch<T>(T command) where T : Command;
    }
}