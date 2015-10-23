using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Events;

namespace Infrastructure.Domain
{
    public abstract class AggregateBase : IAggregate
    {
        public Guid Id { get; protected set; }
        public int Version { get; protected set; }

        private IList<IEvent> uncommittedEvents;
        private IDictionary<Type, Action<IEvent>> eventRoutes;


        public AggregateBase()
        {
            uncommittedEvents = CreateUncommittedEventsList();
            eventRoutes = CreateEventRoutesStore();
        }

        protected IList<IEvent> CreateUncommittedEventsList()
        {
            return new List<IEvent>();
        }

        protected IDictionary<Type, Action<IEvent>> CreateEventRoutesStore()
        {
            return new Dictionary<Type, Action<IEvent>>();
        }
        
        public void RaiseEvent(IEvent @event)
        {
            ApplyEvent(@event);
            uncommittedEvents.Add(@event);
        } 

        public void ApplyEvent(IEvent @event)
        {
            var eventType = @event.GetType();
            if (eventRoutes.ContainsKey(eventType))
            {
                eventRoutes[eventType](@event);
            }
            Version++;
        }

        public void ClearUncommitedEvents()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEvent> GetUncommitedEvents()
        {
            return uncommittedEvents;
        }
    }
}
