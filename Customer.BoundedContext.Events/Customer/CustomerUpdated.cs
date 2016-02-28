using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.Events
{
    public class CustomerUpdated : EventBase
    {
        public string Name { get; set; }

        public CustomerUpdated(Guid id, string name) : base(id)
        {
            Name = name;
        }
    }
}
