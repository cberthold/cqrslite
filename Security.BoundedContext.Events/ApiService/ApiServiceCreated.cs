using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class ApiServiceCreated : EventBase
    {
        public string Name { get; protected set; }
        public ApiServiceCreated(string name)
        {
            Name = name;
        }
    }
}
