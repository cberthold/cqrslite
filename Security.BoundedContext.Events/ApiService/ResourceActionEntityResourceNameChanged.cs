using Infrastructure.Events;
using Security.BoundedContext.Identities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class ResourceActionEntityResourceNameChanged : EventBase
    {
        public ResourceActionId EntityId { get; protected set; }
        public string ResourceName { get; protected set; }

        public ResourceActionEntityResourceNameChanged(ResourceActionId entityId, string resourceName)
        {
            EntityId = entityId;
            ResourceName = resourceName;
        }
    }
}
