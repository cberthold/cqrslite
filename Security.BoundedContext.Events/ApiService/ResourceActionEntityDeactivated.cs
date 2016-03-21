using Infrastructure.Events;
using Security.BoundedContext.Identities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class ResourceActionEntityDeactivated : EventBase
    {
        public ResourceActionId EntityId { get; protected set; }
        
        public ResourceActionEntityDeactivated(ResourceActionId entityId)
        {
            EntityId = entityId;
        }
    }
}
