using Infrastructure.Events;
using Security.BoundedContext.Identities.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events.RootPolicy
{
    public class FeatureAddedToAdminRootPolicy : EventBase
    {
        public FeatureId FeatureId { get; private set; }

        public FeatureAddedToAdminRootPolicy(FeatureId featureId)
        {
            FeatureId = featureId;
        }
    }
}
