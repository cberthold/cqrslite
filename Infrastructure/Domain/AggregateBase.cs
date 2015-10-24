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
        public int Version {
            get { return version; }
            protected set { version = value; }
        }

        private IList<IEvent> uncommittedEvents;
        private IDictionary<Type, Action<IEvent>> eventRoutes;
        private int version = -1;


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

        protected void RegisterTransition<T>(Action<T> transition) where T : class
        {
            eventRoutes.Add(typeof(T), o => transition(o as T));
        }

        public void ClearUncommitedEvents()
        {
            uncommittedEvents.Clear();
        }

        public IEnumerable<IEvent> GetUncommitedEvents()
        {
            return uncommittedEvents;
        }
    }
}
