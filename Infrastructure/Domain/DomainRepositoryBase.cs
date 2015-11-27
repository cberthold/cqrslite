using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Events;


namespace Infrastructure.Domain
{
    public abstract class DomainRepositoryBase : IDomainRepository
    {

        protected int CalculateExpectedVersion<TAggregate>(TAggregate aggregate, List<IEvent<TAggregate>> events)
            where TAggregate : IAggregate<TAggregate>
        {
            var expectedVersion = aggregate.Version - events.Count;
            return expectedVersion;
        }

        protected TAggregate BuildAggregate<TAggregate>(IEnumerable<IEvent<TAggregate>> events) 
            where TAggregate : IAggregate<TAggregate>, new()
        {
            var result = new TAggregate();
            foreach (var @event in events)
            {
                result.ApplyEvent(@event);
            }
            return result;
        }

        public abstract IEnumerable<IEvent<TAggregate>> Save<TAggregate>(TAggregate aggregate) 
            where TAggregate : IAggregate<TAggregate>;

        public abstract TResult GetById<TResult>(Guid id) 
            where TResult : IAggregate<TResult>, new();
        
    }
}
