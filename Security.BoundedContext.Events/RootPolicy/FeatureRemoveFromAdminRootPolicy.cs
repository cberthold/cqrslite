using Infrastructure.Events;
using Security.BoundedContext.Identities.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events.RootPolicy
{
    public class FeatureRemoveFromAdminRootPolicy : EventBase
    {
        public FeatureId FeatureId { get; private set; }

        public FeatureRemoveFromAdminRootPolicy(FeatureId featureId)
        {
            FeatureId = featureId;
        }
    }
}
