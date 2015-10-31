﻿using Customer.BoundedContext.ValueObjects;
using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.Events
{
    public class CustomerBillingAddressUpdated : IEvent
    {
        public Guid Id { get; set; }
        public Address BillingAddress { get; set; }
    }
}