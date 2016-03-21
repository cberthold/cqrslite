using Customer.BoundedContext.Identities;
using Infrastructure.Events;
using System;

namespace Customer.BoundedContext.Events
{
    public class CustomerDeactivated : EventBase
    {
        public CustomerId CustomerId { get; private set; }
        public CustomerDeactivated(CustomerId customerId)
        {
            CustomerId = customerId;
        }
    }
}
