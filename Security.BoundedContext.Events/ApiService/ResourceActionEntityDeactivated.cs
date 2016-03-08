using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class ResourceActionEntityDeactivated : EventBase
    {
        public Guid EntityId { get; protected set; }
        
        public ResourceActionEntityDeactivated(Guid entityId)
        {
            EntityId = entityId;
        }
    }
}
