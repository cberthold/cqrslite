using Customer.BoundedContext.Identities;
using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.Events
{
    public class CustomerCreated : EventBase
    {
        public CustomerId CustomerId { get; private set; }
        public string Name { get; private set; }

        public CustomerCreated(CustomerId customerId, string name)
        {
            CustomerId = customerId;
            Name = name;
        }
        
    }
}
