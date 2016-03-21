using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Identities.RootPolicy
{
    public class AdminRootPolicyId
    {
        public Guid Value { get; private set; }

        public AdminRootPolicyId(Guid id)
        {
            Value = id;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is AdminRootPolicyId)) return false;

            return Value == ((AdminRootPolicyId)obj).Value;
        }
    }
}
