using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.RootPolicy
{
    public class FeaturePolicyMapping : IEntity
    {
        public Guid Id { get; protected set; }
        public Guid FeatureId { get; protected set; }
        public bool? IsEnabled { get; protected set; }

        public FeaturePolicyMapping() { }
    }
}
