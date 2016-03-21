using CQRSlite.Domain;
using Infrastructure.Events;
using Infrastructure.Exceptions;
using Security.BoundedContext.Domain.Feature.Aggregate;
using Security.BoundedContext.Domain.Feature.Entities;
using Security.BoundedContext.Events.RootPolicy;
using Security.BoundedContext.Identities.Feature;
using Security.BoundedContext.Identities.RootPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.RootPolicy.Aggregate
{
    public class AdminRootPolicyAggregate : AggregateRoot
    {
        #region Identity

        public override Guid Id
        {
            get { return PolicyId.Value; }

            protected set
            {
                PolicyId = new AdminRootPolicyId(value);
            }
        }

        public AdminRootPolicyId PolicyId { get; private set; }

        #endregion

        #region State

        private List<FeatureId> availableFeatureList;
        public IList<FeatureId> AvailableFeatures => availableFeatureList.AsReadOnly();


        #endregion

        #region constructors

        private AdminRootPolicyAggregate()
        {
            availableFeatureList = new List<FeatureId>();
        }

        private AdminRootPolicyAggregate(AdminRootPolicyId id) : this()
        {
            this.PolicyId = id;
        }

        #endregion

        #region domain methods

        public void AddFeature(FeatureEntity feature)
        {
            if (this.PolicyAlreadyHasTheFeature(feature.FeatureId)) return;

            ApplyChange(new FeatureAddedToAdminRootPolicy(feature.FeatureId));
        }

        public void RemoveFeature(FeatureEntity feature)
        {
            if (!this.PolicyAlreadyHasTheFeature(feature.FeatureId)) return;

            ApplyChange(new FeatureRemoveFromAdminRootPolicy(feature.FeatureId));
        }

        #endregion

        #region Factory methods

        public static AdminRootPolicyAggregate Create()
        {
            var policyId = new AdminRootPolicyId(Guid.Parse("E52BADD5-12DF-4A01-ADD0-CF5EE52BDCAF"));
            var aggregate = new AdminRootPolicyAggregate(policyId);
            return aggregate;
        }

        #endregion

        #region Apply State Changes

        public void Apply(AdminRootPolicyInitialized @event)
        {
            var policyId = new AdminRootPolicyId(@event.PolicyId);
            PolicyId = policyId;
        }

        public void Apply(FeatureAddedToAdminRootPolicy @event)
        {
            if (availableFeatureList.Contains(@event.FeatureId)) return;

            availableFeatureList.Add(@event.FeatureId);
        }

        public void Apply(FeatureRemoveFromAdminRootPolicy @event)
        {
            if (!availableFeatureList.Contains(@event.FeatureId)) return;

            availableFeatureList.Remove(@event.FeatureId);
        }

        #endregion

    }

    public static class AdminRootPolicyAggregateExtensions
    {
        public static bool PolicyAlreadyHasTheFeature(this AdminRootPolicyAggregate aggregate, FeatureId feature)
        {
            var features = aggregate.AvailableFeatures;
            var exists = features.Any(existing => feature == existing);

            return exists;
        }
    }
    
    
}
