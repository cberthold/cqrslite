using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class ResourceActionEntityRemoved : EventBase
    {
        public Guid EntityId { get; protected set; }
        public Guid ApiServiceId { get; protected set; }

        public ResourceActionEntityRemoved(Guid apiServiceId, Guid entityId)
        {
            ApiServiceId = apiServiceId;
            EntityId = entityId;
        }
    }
}
