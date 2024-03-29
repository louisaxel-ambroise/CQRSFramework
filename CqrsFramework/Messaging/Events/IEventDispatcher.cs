﻿namespace CqrsFramework.Messaging.Events
{
    public interface IEventDispatcher
    {
        void Dispatch<T>(T @event) where T : Event;
    }
}