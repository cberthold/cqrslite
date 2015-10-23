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
        IEnumerable<IEvent> Save<TAggregate>(TAggregate aggregate) where TAggregate : IAggregate;
        TResult GetById<TResult>(Guid id) where TResult : IAggregate, new();
    }
}
