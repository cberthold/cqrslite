using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public interface IAggregate
    {
        IEnumerable<IEvent> GetUncommitedEvents();
        void ClearUncommitedEvents();
        int Version { get; }
        Guid Id { get; }
        void ApplyEvent(IEvent @event);
    }
}
