using Infrastructure.Events;
using Security.BoundedContext.Identities.Api;
using Security.BoundedContext.Identities.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events.Feature
{
    public class ResourceActionRemovedFromFeature : EventBase
    {
        public FeatureId FeatureId { get; private set; }
        public ResourceActionId ResourceActionId { get; private set; }

        public ResourceActionRemovedFromFeature(FeatureId featureId, ResourceActionId resourceActionId)
        {
            FeatureId = FeatureId;
            ResourceActionId = resourceActionId;
        }
    }
}
