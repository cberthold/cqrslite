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
        public string Name { get; set; }

        public CustomerCreated(Guid id, string name) : base(id)
        {
            Name = name;
        }
        
    }
}
