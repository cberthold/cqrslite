using CQRSlite.Domain;
using Infrastructure.Events;
using Infrastructure.Exceptions;
using Security.BoundedContext.Domain.Feature.Aggregate;
using Security.BoundedContext.Domain.Feature.Entities;
using Security.BoundedContext.Domain.RootPolicy.Entities;
using Security.BoundedContext.Domain.User.Aggregate;
using Security.BoundedContext.Events.RootPolicy;
using Security.BoundedContext.Identities.Feature;
using Security.BoundedContext.Identities.RootPolicy;
using Security.BoundedContext.Identities.User;
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

        private List<AdminChildPolicyEntity> childPolicies;
        public IList<AdminChildPolicyEntity> ChildPolicies => childPolicies.AsReadOnly();
        
        #endregion

        #region Constructors

        private AdminRootPolicyAggregate()
        {
            availableFeatureList = new List<FeatureId>();
            childPolicies = new List<AdminChildPolicyEntity>();
        }

        private AdminRootPolicyAggregate(AdminRootPolicyId id) : this()
        {
            this.PolicyId = id;
        }

        #endregion

        #region Domain methods

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

        public void AddChildPolicy(string policyName)
        {
            var id = new AdminChildPolicyId(PolicyId, Guid.NewGuid());
            ApplyChange(new AdminChildPolicyAdded(id, policyName));
        }

        public void LinkAdminChildPolicyToUser(AdminChildPolicyEntity childPolicy, UserAggregate user)
        {
            if (childPolicy == null)
                throw new DomainException($"{nameof(childPolicy)} cannot be null");
            if (user == null)
                throw new DomainException($"{nameof(user)} cannot be null");
            if (!childPolicies.Contains(childPolicy))
                throw new DomainException($"policy {Id} does not contain child policy {childPolicy.Id}");

            var userPolicyId = user.AdminPolicy.UserPolicyId;
            var childPolicyId = childPolicy.ChildPolicyId;

            ApplyChange(new UserLinkedToChildPolicy(childPolicyId, userPolicyId));
        }

        public void UnlinkAdminChildPolicyFromUser(AdminChildPolicyEntity childPolicy, UserAggregate user)
        {
            if (childPolicy == null)
                throw new DomainException($"{nameof(childPolicy)} cannot be null");
            if (user == null)
                throw new DomainException($"{nameof(user)} cannot be null");
            if (!childPolicies.Contains(childPolicy))
                throw new DomainException($"policy {Id} does not contain child policy {childPolicy.Id}");

            var userPolicyId = user.AdminPolicy.UserPolicyId;
            var childPolicyId = childPolicy.ChildPolicyId;

            ApplyChange(new UserUnlinkedFromChildPolicy(childPolicyId, userPolicyId));
        }

        public AdminChildPolicyEntity FindChildPolicy(AdminChildPolicyId childPolicyId)
        {
            if (childPolicyId == null)
                throw new DomainException($"{nameof(childPolicyId)} cannot be null");
            if (childPolicyId.Value != Id)
                throw new DomainException($"{nameof(childPolicyId)} with Id {childPolicyId.PolicyIdValue} does not relate to the Id {Id}");

            var childPolicy = ChildPolicies.FirstOrDefault(a => a.ChildPolicyId == childPolicyId);
            return childPolicy;
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

            UpdateFeaturesOnPolicies();
        }

        public void Apply(FeatureRemoveFromAdminRootPolicy @event)
        {
            if (!availableFeatureList.Contains(@event.FeatureId)) return;

            availableFeatureList.Remove(@event.FeatureId);

            UpdateFeaturesOnPolicies();
        }

        public void Apply(AdminChildPolicyAdded @event)
        {
            var entity = new AdminChildPolicyEntity(@event.PolicyId, @event.Name);
            childPolicies.Add(entity);
            UpdateFeaturesOnPolicy(entity);
        }

        public void Apply(UserLinkedToChildPolicy @event)
        {
            var childPolicy = childPolicies.FirstOrDefault(a => a.ChildPolicyId == @event.AdminChildPolicyId);
            if(childPolicy != null)
            {
                childPolicy.AddUserLink(@event.UserPolicyId);
            }

        }

        public void Apply(UserUnlinkedFromChildPolicy @event)
        {
            var childPolicy = childPolicies.FirstOrDefault(a => a.ChildPolicyId == @event.AdminChildPolicyId);
            if (childPolicy != null)
            {
                childPolicy.RemoveUserLink(@event.UserPolicyId);
            }

        }

        #endregion

        #region Private methods

        private void UpdateFeaturesOnPolicies()
        {
            var availableFeatures = availableFeatureList.ToArray();
            foreach (var policy in childPolicies)
            {
                UpdateFeaturesOnPolicy(policy, availableFeatures);
            }
        }

        private void UpdateFeaturesOnPolicy(AdminChildPolicyEntity childPolicy, FeatureId[] features = null)
        {
            if (features == null)
            {
                features = availableFeatureList.ToArray();
            }
            childPolicy.UpdateFeatures(features);

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
