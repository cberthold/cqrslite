﻿using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class ResourceActionEntityCreated : EventBase
    {
        public Guid EntityId { get; protected set; }
        public string ResourceName { get; protected set; }
        public string ActionName { get; protected set; }
        
        public ResourceActionEntityCreated(Guid entityId, string resourceName, string actionName)
        {
            EntityId = entityId;
            ResourceName = resourceName;
            ActionName = actionName;
        }
    }
}