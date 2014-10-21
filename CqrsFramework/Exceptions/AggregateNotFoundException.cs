using System;

namespace CqrsFramework.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public readonly Guid AggregateId;

        public AggregateNotFoundException(Guid aggregateId)
            : base(string.Format("Aggregate with ID '{0:D}' was not found.", aggregateId))
        {
            AggregateId = aggregateId;
        }

    }
}