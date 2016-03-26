using Infrastructure.Domain;
using Security.BoundedContext.Identities.Feature;
using Security.BoundedContext.Identities.RootPolicy;
using Security.BoundedContext.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.BoundedContext.Identities.User;
using Security.BoundedContext.Identities.Common;

namespace Security.BoundedContext.Domain.RootPolicy.Entities
{
    public class AdminChildPolicyEntity : IEntity
    {
        #region Identity

        public Guid Id => ChildPolicyId.Value;
        public AdminChildPolicyId ChildPolicyId { get; private set; }

        #endregion

        #region State

        public string Name { get; private set; }

        public IDictionary<FeatureId, FeatureState> FeatureEnablement => featureEnablements.AsReadOnly();
        private IDictionary<FeatureId, FeatureState> featureEnablements;

        private List<AdminUserPolicyId> userPolicies;
        public IList<AdminUserPolicyId> UserPolicies => userPolicies.AsReadOnly();

        #endregion

        #region constructors
        private AdminChildPolicyEntity()
        {
            featureEnablements = new Dictionary<FeatureId, FeatureState>();
            userPolicies = new List<AdminUserPolicyId>();
        }

        public AdminChildPolicyEntity(AdminChildPolicyId childPolicyId, string policyName)
        {
            ChildPolicyId = childPolicyId;
            Name = policyName;
        }

        #endregion

        #region domain methods

        public void UpdateFeatures(params FeatureId[] featuresToAddOrRemove)
        {
            var existingFeatures = featureEnablements.Keys;
            var updatedFeatures = featuresToAddOrRemove;

            var featuresToRemove = existingFeatures.Except(updatedFeatures);

            foreach (var feature in featuresToRemove)
            {
                if (featureEnablements.ContainsKey(feature))
                    featureEnablements.Remove(feature);
            }

            var featuresToAdd = updatedFeatures.Except(featureEnablements.Keys);

            foreach (var feature in featuresToAdd)
            {
                if (!featureEnablements.ContainsKey(feature))
                    featureEnablements.Add(feature, FeatureState.Inherited);
            }
        }

        internal void AddUserLink(AdminUserPolicyId userPolicyId)
        {
            if (!userPolicies.Contains(userPolicyId))
                userPolicies.Add(userPolicyId);
        }

        internal void RemoveUserLink(AdminUserPolicyId userPolicyId)
        {
            if (userPolicies.Contains(userPolicyId))
                userPolicies.Remove(userPolicyId);
        }

        #endregion
    }

}
