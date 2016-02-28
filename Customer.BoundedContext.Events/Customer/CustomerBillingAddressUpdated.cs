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
        public Address BillingAddress { get; set; }

        public CustomerBillingAddressUpdated(Guid id, Address billingAddress) : base(id)
        {
            BillingAddress = billingAddress;
        }
    }
}
