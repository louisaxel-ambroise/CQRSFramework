namespace CqrsFramework.Messaging.Events
{
    public interface IEventHandler { }

    public interface IEventHandler<in T> : IEventHandler where T : Event
    {
        void Handle(T @event);
    }
}