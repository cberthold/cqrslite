using Infrastructure.Domain;
using Security.BoundedContext.Identities.Feature;
using Security.BoundedContext.Identities.RootPolicy;
using Security.BoundedContext.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #endregion

        #region constructors
        private AdminChildPolicyEntity()
        {
            featureEnablements = new Dictionary<FeatureId, FeatureState>();
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

        #endregion
    }

}
