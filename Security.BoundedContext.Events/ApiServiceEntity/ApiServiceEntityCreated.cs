using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class ApiServiceEntityCreated : EventBase
    {
        public Guid EntityId { get; protected set; }
        public string Name { get; protected set; }
        public ApiServiceEntityCreated(Guid entityId, string name)
        {
            this.EntityId = entityId;
            this.Name = name;
        }
    }
}
