using Customer.BoundedContext.Identities;
using Customer.BoundedContext.ValueObjects;
using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.Events
{
    public class CustomerBillingAddressUpdated : EventBase
    {
        public CustomerId CustomerId { get; private set; }
        public Address BillingAddress { get; set; }

        public CustomerBillingAddressUpdated(CustomerId customerId, Address billingAddress)
        {
            CustomerId = customerId;
            BillingAddress = billingAddress;
        }
    }
}
