using Infrastructure.Events;
using System;

namespace Customer.BoundedContext.Events
{
    public class CustomerActivated : EventBase
    {
        public CustomerActivated(Guid id) : base(id)
        { }
    }
}
