using Customer.BoundedContext.Identities;
using Infrastructure.Events;
using System;

namespace Customer.BoundedContext.Events
{
    public class CustomerActivated : EventBase
    {
        public CustomerId CustomerId { get; private set; }
        public CustomerActivated(CustomerId customerId)
        {
            CustomerId = customerId;
        }
    }
}
