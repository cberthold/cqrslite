using CQRSlite.Domain;
using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    public abstract class RootPolicyAggregate<TType> : AggregateRoot
        where TType : IIdentity
    {
        
        protected List<ResourceActionEntity> availableResourceActions 
            = new List<ResourceActionEntity>();
        public IList<ResourceActionEntity> AvailableResourceActions 
            => availableResourceActions.AsReadOnly();
        
        protected List<Feature> features 
            = new List<Feature>();
        public IList<Feature> Features 
            => features.AsReadOnly();

        protected List<UserMappedPolicy<TType>> userMappedPolicies 
            = new List<UserMappedPolicy<TType>>();
        public IList<UserMappedPolicy<TType>> UserMappedPolicies 
            => userMappedPolicies.AsReadOnly();

        protected RootPolicyAggregate()
        {
        }
    }
}
