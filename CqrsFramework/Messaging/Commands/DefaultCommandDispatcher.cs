using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using ReflectionMagic;

namespace CqrsFramework.Messaging.Commands
{
    public sealed class DefaultCommandDispatcher : ICommandDispatcher
    {
        private readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly ICommandHandler[] _handlers;

        public DefaultCommandDispatcher(ICommandHandler[] handlers)
        {
            _handlers = handlers;
        }

        public void Dispatch<T>(T command) where T : Command
        {
            try
            {
                var handler = GetCommandHandler<T>().SingleOrDefault();

                if (handler != null)
                {
                    handler.AsDynamic().Handle(command);
                }
                else
                {
                    Log.Warn("No handler found for command '{0}'", typeof (T).Name);
                }
            }
            catch
            {
                throw new ArgumentException("Unable to dispatch command to more than one handler.");
            }
        }

        private IEnumerable<ICommandHandler> GetCommandHandler<T>() where T : Command
        {
            return _handlers.Where(h => h.GetType().GetInterfaces().Any(x => x.Name == typeof(ICommandHandler<>).Name && x.GenericTypeArguments[0].Name == typeof(T).Name)).ToList();
        }
    }
}