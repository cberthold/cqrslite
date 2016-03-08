using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class ResourceActionEntityActivated : EventBase
    {
        public Guid EntityId { get; protected set; }
        
        public ResourceActionEntityActivated(Guid entityId)
        {
            EntityId = entityId;
        }
    }
}
