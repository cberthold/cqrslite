using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Identities.Feature
{
    public class FeatureId
    {
        public Guid Value { get; private set; }

        public FeatureId(Guid id)
        {
            Value = id;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is FeatureId)) return false;

            return Value == ((FeatureId)obj).Value;
        }
    }
}
