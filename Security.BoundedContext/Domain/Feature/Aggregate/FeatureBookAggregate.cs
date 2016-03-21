using CQRSlite.Domain;
using Infrastructure.Domain;
using Infrastructure.Exceptions;
using Security.BoundedContext.Domain.Api.Entities;
using Security.BoundedContext.Domain.Feature.Entities;
using Security.BoundedContext.Events;
using Security.BoundedContext.Events.Feature;
using Security.BoundedContext.Identities.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.Feature.Aggregate
{
    public class FeatureBookAggregate : AggregateRoot
    {

        #region Identity
        public override Guid Id
        {
            get { return FeatureBookId.Value; }
            protected set { FeatureBookId = new FeatureBookId(value); }
        }

        public FeatureBookId FeatureBookId { get; protected set; }

        #endregion

        #region State

        protected List<FeatureEntity> features;
        public IList<FeatureEntity> Features => features.AsReadOnly();



        #endregion

        #region constructors

        public FeatureBookAggregate() : base()
        {
            features = new List<FeatureEntity>();
        }

        private FeatureBookAggregate(FeatureBookId featureBookId) : this()
        {
            FeatureBookId = featureBookId;

        }

        #endregion

        #region factory methods

        public static FeatureBookAggregate Create()
        {
            var newId = new FeatureBookId(Guid.NewGuid());
            var feature = new FeatureBookAggregate(newId);
            return feature;
        }

        #endregion

        #region domain methods

        public FeatureEntity AddFeature(string featureName)
        {
            var id = new FeatureId(Guid.NewGuid());
            ApplyChange(new FeatureCreated(FeatureBookId, id, featureName));
            var feature = GetFeature(id);

            ActivateFeature(feature);

            return feature;
        }

        public void ActivateFeature(FeatureEntity feature)
        {
            if (!this.HasTheExistingFeature(feature))
                throw new DomainException($"The feature book with {FeatureBookId} does not contain the feature \'{feature.Name}\' and id {feature.FeatureId}");

            // if we are active then just remain indempotent since
            // state wont have to change
            var existingFeatureId = feature.FeatureId;
            var existingFeature = GetFeature(existingFeatureId);

            if (existingFeature.IsActive) return;

            ApplyChange(new FeatureActivated(existingFeatureId));
        }

        public void DeactivateFeature(FeatureEntity feature)
        {
            if (!this.HasTheExistingFeature(feature))
                throw new DomainException($"The feature book with {FeatureBookId} does not contain the feature \'{feature.Name}\' and id {feature.FeatureId}");

            var existingFeatureId = feature.FeatureId;
            var existingFeature = GetFeature(existingFeatureId);

            if (!existingFeature.IsActive) return;

            ApplyChange(new FeatureDeactivated(existingFeatureId));
        }

        public void AddResourceActionToFeature(FeatureEntity feature, ResourceActionEntity resourceAction)
        {
            if (!this.HasTheExistingFeature(feature))
                throw new DomainException($"The feature book with {FeatureBookId} does not contain the feature \'{feature.Name}\' and id {feature.FeatureId}");

            var existingFeatureId = feature.FeatureId;
            var existingFeature = GetFeature(existingFeatureId);

            if (existingFeature.HasTheExistingResourceAction(resourceAction)) return;

            ApplyChange(new ResourceActionAddedToFeature(feature.FeatureId, resourceAction.ResourceActionId));

        }

        public void RemoveResourceActionFromFeature(FeatureEntity feature, ResourceActionEntity resourceActionToRemove)
        {
            if (!this.HasTheExistingFeature(feature))
                throw new DomainException($"The feature book with {FeatureBookId} does not contain the feature \'{feature.Name}\' and id {feature.FeatureId}");

            var existingFeatureId = feature.FeatureId;
            var existingFeature = GetFeature(existingFeatureId);

            if (!existingFeature.HasTheExistingResourceAction(resourceActionToRemove)) return;

            ApplyChange(new ResourceActionRemovedFromFeature(feature.FeatureId, resourceActionToRemove.ResourceActionId));

        }


        #endregion

        #region private methods

        public FeatureEntity GetFeature(FeatureId featureId)
        {
            return features.FirstOrDefault(feature => feature.FeatureId == featureId);
        }

        #endregion

        #region apply state

        public void Apply(FeatureCreated @event)
        {
            features.Add(new FeatureEntity(@event.FeatureBookId, @event.FeatureId, @event.Name));
        }

        public void Apply(FeatureActivated @event)
        {
            var feature = GetFeature(@event.FeatureId);
            feature.SetIsActive(true);
        }

        public void Apply(FeatureDeactivated @event)
        {
            var feature = GetFeature(@event.FeatureId);
            feature.SetIsActive(false);
        }

        public void Apply(ResourceActionAddedToFeature @event)
        {
            var feature = GetFeature(@event.FeatureId);

            feature.AddResourceAction(@event.ResourceActionId);

        }

        public void Apply(ResourceActionRemovedFromFeature @event)
        {
            var feature = GetFeature(@event.FeatureId);

            feature.RemoveResourceAction(@event.ResourceActionId);
        }

        #endregion

    }


    public static class FeatureBookAggregateExtensions
    {
        public static bool HasTheExistingFeature(this FeatureBookAggregate aggregate, FeatureEntity feature)
        {
            return DoesFeatureExist(aggregate, feature.FeatureId);
        }

        public static bool DoesFeatureExist(this FeatureBookAggregate aggregate, FeatureId featureId)
        {
            return aggregate.Features.Any(existing => existing.FeatureId == featureId);
        }

        public static bool HasTheExistingResourceAction(this FeatureEntity feature, ResourceActionEntity resourceAction)
        {
            return feature.ResourceActions.Any(existing => existing == resourceAction.ResourceActionId);
        }
    }
}
