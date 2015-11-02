using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.Events
{
    public class CustomerCreated : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        
    }
}
