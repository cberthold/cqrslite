using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.RootPolicy
{
    public abstract class PolicyBase : IEntity
    {
        public Guid Id { get; private set; }

        private List<FeatureToAddToPolicy> _availableFeatures;
        public IList<FeatureToAddToPolicy> AvailableFeatures { get { return _availableFeatures.AsReadOnly(); } }

        protected PolicyBase(Guid id)
        {
            _availableFeatures = new List<FeatureToAddToPolicy>();
            Id = id;
        }
    }
}
