using Infrastructure.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Events;

namespace Infrastructure.Domain
{
    public abstract class AggregateBase<TAggregate> : IAggregate<TAggregate>, IEquatable<IAggregate<TAggregate>>
        where TAggregate : AggregateBase<TAggregate>, IAggregate<TAggregate>
    {
        private readonly ICollection<IEvent<TAggregate>> uncommittedEvents = new LinkedList<IEvent<TAggregate>>();

        private IRouteEvents<TAggregate> registeredRoutes;

        protected AggregateBase()
            : this(null)
        { }

        protected AggregateBase(IRouteEvents<TAggregate> handler)
        {
            if (handler == null)
            {
                return;
            }

            this.RegisteredRoutes = handler;
            this.RegisteredRoutes.Register(this);
        }

        protected IRouteEvents<TAggregate> RegisteredRoutes
        {
            get
            {
                return this.registeredRoutes ?? (this.registeredRoutes = new ConventionEventRouter<TAggregate>(true, this));
            }
            set
            {
                if (value == null)
                {
                    throw new InvalidOperationException("AggregateBase must have an event router to function");
                }

                this.registeredRoutes = value;
            }
        }

        public Guid Id { get; protected set; }

        public int Version { get; protected set; }
        

        protected void Register<T>(Action<T> route)
        {
            this.RegisteredRoutes.Register(route);
        }

        protected void RaiseEvent(IEvent<TAggregate> @event)
        {
            ApplyEvent(@event);
            this.uncommittedEvents.Add(@event);
        }

        protected virtual IMemento GetSnapshot()
        {
            return null;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as IAggregate<TAggregate>);
        }

        public void ApplyEvent(IEvent<TAggregate> @event)
        {
            this.RegisteredRoutes.Dispatch(@event);
            this.Version++;
        }

        public ICollection<IEvent<TAggregate>> GetUncommittedEvents()
        {
            return uncommittedEvents;
        }

        public void ClearUncommittedEvents()
        {
            uncommittedEvents.Clear();
        }

        IMemento IAggregate<TAggregate>.GetSnapshot()
        {
            IMemento snapshot = this.GetSnapshot();
            snapshot.Id = this.Id;
            snapshot.Version = this.Version;
            return snapshot;
        }

        public bool Equals(IAggregate<TAggregate> other)
        {
            return null != other && other.Id == this.Id;
        }
        
    }
}
