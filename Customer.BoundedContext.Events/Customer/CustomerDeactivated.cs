using Infrastructure.Events;
using System;

namespace Customer.BoundedContext.Events
{
    public class CustomerDeactivated : EventBase
    {
        public CustomerDeactivated(Guid id) : base(id)
        {

        }
    }
}
