using System.Collections.Generic;
using System.Linq;
using ReflectionMagic;

namespace CqrsFramework.Messaging.Events
{
    public sealed class DefaultEventDispatcher : IEventDispatcher
    {
        private readonly IEventHandler[] _handlers;

        public DefaultEventDispatcher(IEventHandler[] handlers)
        {
            _handlers = handlers;
        }

        public void Dispatch<T>(T @event) where T : Event
        {
            var handlers = GetEventHandlers<T>();

            foreach (var eventHandler in handlers)
            {
                eventHandler.AsDynamic().Handle(@event);
            }
        }

        private IEnumerable<IEventHandler> GetEventHandlers<T>()
        {
            return _handlers.Where(h => h.GetType().GetInterfaces().Any(x => x.Name == typeof(IEventHandler<>).Name && x.GenericTypeArguments[0].Name == typeof(T).Name)).ToList();
        }
    }
}