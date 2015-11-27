
using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public interface IDomainRepository
    {
        IEnumerable<IEvent<TAggregate>> Save<TAggregate>(TAggregate aggregate)
            where TAggregate : IAggregate<TAggregate>;
        TResult GetById<TResult>(Guid id) where TResult : IAggregate<TResult>, new();
    }
}
