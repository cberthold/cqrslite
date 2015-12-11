using Customer.BoundedContext.Domain;
using Infrastructure.Events;
using System;

namespace Customer.BoundedContext.Events
{
    public class CustomerActivated : EventBase<CustomerAggregate>
    {
        public CustomerActivated(Guid id) : base(id)
        { }
    }
}
