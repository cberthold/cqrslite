using CQRSlite.Domain;
using Infrastructure.Domain;
using Infrastructure.Exceptions;
using Security.BoundedContext.Domain.Api.Entities;
using Security.BoundedContext.Domain.Feature.Entities;
using Security.BoundedContext.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.Feature.Aggregate
{
    public class FeatureAggregate : AggregateRoot
    {
        public string Name { get; protected set; }
        public bool IsActive { get; protected set; }

        protected List<AddedResourceAction> resourceActions;
        public IList<AddedResourceAction> ResourceActions => resourceActions.AsReadOnly();

        public FeatureAggregate() : base()
        {
            resourceActions = new List<AddedResourceAction>();
        }

        private FeatureAggregate(Guid id, string name) : this()
        {
            Id = id;
            ApplyChange(new FeatureCreated(name));
            ApplyChange(new FeatureActivated());
        }

        public static FeatureAggregate Create(string name)
        {
            var newId = Guid.NewGuid();
            var feature =  new FeatureAggregate(newId, name);
            return feature;
        }

        public void ActivateFeature()
        {
            if (IsActive)
                throw new DomainException("Feature already activated");

            ApplyChange(new FeatureActivated());
        }

        public void DeactivateFeature()
        {
            if (!IsActive)
                throw new DomainException("Feature already deactivated");

            ApplyChange(new FeatureDeactivated());
        }

        public void AddResourceActionToFeature(ResourceActionEntity resourceAction)
        {
            if(resourceActions.Any(a=> a.ResourceActionEntityId == resourceAction.Id))
                throw new DomainException("duplicate resource action");

            ApplyChange(new ResourceActionAddedToFeature(resourceAction.ApiServiceId, resourceAction.Id));
            
        }

        public void Apply(FeatureCreated @event)
        {
            Name = @event.Name;
        }

        public void Apply(FeatureActivated @event)
        {
            IsActive = true;
        }

        public void Apply(FeatureDeactivated @event)
        {
            IsActive = false;
        }

        public void Apply(ResourceActionAddedToFeature @event)
        {
            if (resourceActions.Any(a => a.ApiServiceId == @event.ApiServiceId && a.ResourceActionEntityId == @event.ResourceActionEntityId))
                return;

            resourceActions.Add(new AddedResourceAction(@event.ApiServiceId, @event.ResourceActionEntityId));
        }
    }
}
