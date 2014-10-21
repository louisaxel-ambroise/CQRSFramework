using System;
using CqrsFramework.Aggregate;

namespace CqrsFramework.Repository
{
    public interface IRepository<TAggregate> where TAggregate : AggregateRoot, new()
    {
        TAggregate GetById(Guid id);
        void Save(TAggregate aggregate);
    }
}