using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class ResourceActionAddedToFeature : EventBase
    {
        public Guid ApiServiceId { get; private set; }
        public Guid ResourceActionEntityId { get; private set; }

        public ResourceActionAddedToFeature(Guid apiServiceId, Guid resourceActionEntityId)
        {
            ApiServiceId = apiServiceId;
            ResourceActionEntityId = resourceActionEntityId;
        }
    }
}
