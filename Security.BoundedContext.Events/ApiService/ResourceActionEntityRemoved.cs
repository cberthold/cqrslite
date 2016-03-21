using Infrastructure.Events;
using Security.BoundedContext.Identities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class ResourceActionEntityRemoved : EventBase
    {
        public ResourceActionId EntityId { get; protected set; }
        
        public ResourceActionEntityRemoved(ResourceActionId entityId)
        {
            EntityId = entityId;
        }
    }
}
