using Customer.BoundedContext.Domain;
using Infrastructure.Events;
using System;

namespace Customer.BoundedContext.Events
{
    public class CustomerDeactivated : EventBase<CustomerDeactivated, CustomerAggregate>
    {
        public CustomerDeactivated(Guid id) : base(id)
        {

        }
    }
}
