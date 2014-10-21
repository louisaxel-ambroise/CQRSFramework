namespace CqrsFramework.Messaging.Events
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : Event;
    }
}