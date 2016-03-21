using Infrastructure.Events;
using Security.BoundedContext.Identities.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events
{
    public class FeatureCreated : EventBase
    {
        public FeatureBookId FeatureBookId { get; private set; }
        public FeatureId FeatureId { get; private set; }
        public string Name { get; private set; }

        public FeatureCreated(FeatureBookId featureBookId, FeatureId featureId, string name)
        {
            FeatureBookId = featureBookId;
            FeatureId = featureId;
            Name = name;
        }
    }
}
