using Infrastructure.Events;
using System;

namespace Customer.BoundedContext.Events
{
    public class CustomerActivated : IEvent
    {
        public Guid Id { get; set; }
    }
}
