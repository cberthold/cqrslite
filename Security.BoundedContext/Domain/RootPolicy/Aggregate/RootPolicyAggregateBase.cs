using CQRSlite.Domain;
using Infrastructure.Domain;
using Infrastructure.Exceptions;
using Security.BoundedContext.Domain.RootPolicy;
using Security.BoundedContext.Domain.RootPolicy.Aggregate;
using Security.BoundedContext.Domain.RootPolicy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    public class RootPolicyAggregate<TRoot> : AggregateRoot, IPolicy
        where TRoot : RootPolicyAggregate<TRoot>
    {

        public Guid? ParentPolicyId { get; private set; }
        public TRoot ParentPolicy { get; private set; }
        public bool IsRootPolicy => ParentPolicyId == null;

        //private IDictionary<Guid, FeatureToAddToPolicy> _addedFeatures;
        //public IDictionary<Guid, FeatureToAddToPolicy> AddedFeatures => _addedFeatures.AsReadOnly();

        private List<TRoot> _childPolicies;
        public IList<TRoot> ChildPolicies => _childPolicies.AsReadOnly();


        

        protected RootPolicyAggregate()
        {
            //_addedFeatures = new Dictionary<Guid, FeatureToAddToPolicy>();
            _childPolicies = new List<TRoot>();
        }

        //public virtual void AddFeatureToPolicy(FeatureAggregate feature)
        //{
        //    var featureId = feature.Id;
        //    if (_addedFeatures.ContainsKey(featureId))
        //        throw new DomainException($"Duplicate added feature id { featureId }");

        //    if (_addedFeatures.Any(a => a.Value.Feature.Id == featureId))
        //        throw new DomainException($"Duplicate feature { feature.Name } : { featureId }");

        //    var featureToAdd = new FeatureToAddToPolicy(this, feature);

        //    _addedFeatures[featureId] = featureToAdd;

        //    foreach (var child in _childPolicies)
        //        child.AddFeatureToPolicy(feature);
        //}

        internal virtual void LoadParentPolicies(IPolicyService policyService)
        {
            var currentPolicy = this;
            while(true)
            {
                if (currentPolicy == null || currentPolicy.IsRootPolicy) break;

                var parentPolicyId = currentPolicy.ParentPolicyId.Value;
                currentPolicy = currentPolicy.ParentPolicy = policyService.LoadRootPolicy<TRoot>(parentPolicyId);
                
            }
        }
    }
}
