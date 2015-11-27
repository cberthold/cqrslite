using Infrastructure.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public interface IAggregate<TAggregate>
        where TAggregate : IAggregate<TAggregate>
    {
        Guid Id { get; }
        int Version { get; }

        void ApplyEvent(IEvent<TAggregate> @event);

        ICollection<IEvent<TAggregate>> GetUncommittedEvents();

        void ClearUncommittedEvents();

        IMemento GetSnapshot();
    }

}
