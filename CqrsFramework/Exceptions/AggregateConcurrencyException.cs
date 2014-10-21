using System;

namespace CqrsFramework.Exceptions
{
    public class AggregateConcurrencyException : Exception
    {
        public AggregateConcurrencyException(Guid aggregateId, int expectedVersion, int currentVersion)
            : base(string.Format("Aggregate '{0:D}' is in version {1:D} (expected {2:D})", aggregateId, currentVersion, expectedVersion))
        {
            AggregateId = aggregateId;
        }

        public Guid AggregateId { get; set; }
    }
}