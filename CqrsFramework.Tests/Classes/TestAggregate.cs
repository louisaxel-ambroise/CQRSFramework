using CqrsFramework.Aggregate;

namespace CqrsFramework.Tests.Classes
{
    public class TestAggregate : AggregateRoot
    {
        public int AppliedEvents { get; set; }

        /// <summary>
        /// Handles the SomethingDoneEvent events.
        /// </summary>
        /// <param name="event">The event to handle</param>
        public void Handle(SomethingDoneEvent @event)
        {
            Id = @event.Id;
            AppliedEvents++;
        }

        public void Handle(AggregateEvent @event)
        {
            AppliedEvents++;
        }
    }
}