using Infrastructure.Domain;
using Security.BoundedContext.Identities.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.BoundedContext.Identities.Api;

namespace Security.BoundedContext.Domain.Feature.Entities
{
    public class FeatureEntity : IEntity
    {
        public Guid Id => FeatureId.Value;

        public FeatureBookId FeatureBookId { get; private set; }
        public FeatureId FeatureId { get; private set; }

        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        protected List<ResourceActionId> resourceActions;
        public IList<ResourceActionId> ResourceActions => resourceActions.AsReadOnly();

        private FeatureEntity()
        {
            resourceActions = new List<ResourceActionId>();
        }

        internal FeatureEntity(FeatureBookId featureBookId, FeatureId featureId, string name)
            : this()
        {
            FeatureBookId = featureBookId;
            FeatureId = featureId;
            Name = name;

        }

        internal void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        internal void AddResourceAction(ResourceActionId resourceActionId)
        {
            if (resourceActions.Contains(resourceActionId)) return;

            resourceActions.Add(resourceActionId);
        }

        internal void RemoveResourceAction(ResourceActionId resourceActionId)
        {
            if (!resourceActions.Contains(resourceActionId)) return;

            resourceActions.Remove(resourceActionId);
        }
    }

    
}
