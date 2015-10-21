using CommonDomain.Core;
using Customer.BoundedContext.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.Model
{
    public class Customer : AggregateBase
    {
        public string Name { get; private set; }
        public Address Address { get; private set; }
    }
}
