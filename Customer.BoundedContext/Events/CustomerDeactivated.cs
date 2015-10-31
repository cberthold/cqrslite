using Infrastructure.Events;
using System;

namespace Customer.BoundedContext.Events
{
    public class CustomerDeactivated : IEvent
    {
        public Guid Id { get; set; }
    }
}
