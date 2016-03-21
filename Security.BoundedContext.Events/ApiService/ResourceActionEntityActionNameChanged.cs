using Infrastructure.Events;
using Security.BoundedContext.Identities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class ResourceActionEntityActionNameChanged : EventBase
    {
        public ResourceActionId EntityId { get; protected set; }
        public string ActionName { get; protected set; }

        public ResourceActionEntityActionNameChanged(ResourceActionId entityId, string actionName)
        {
            EntityId = entityId;
            ActionName = actionName;
        }
    }
}
