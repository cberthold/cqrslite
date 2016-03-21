using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Identities.Feature
{
    public class FeatureBookId
    {
        public Guid Value { get; private set; }

        public FeatureBookId(Guid id)
        {
            Value = id;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is FeatureBookId)) return false;

            return Value == ((FeatureBookId)obj).Value;
        }
    }
}
