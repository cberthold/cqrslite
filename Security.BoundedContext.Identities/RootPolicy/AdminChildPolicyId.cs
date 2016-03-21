using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Identities.RootPolicy
{
    public class AdminChildPolicyId
    {
        public Guid PolicyIdValue { get; private set; }
        public Guid Value { get; private set; }

        public AdminChildPolicyId(AdminRootPolicyId policyId, Guid id)
        {
            PolicyIdValue = policyId.Value;
            Value = id;
        }

        public override int GetHashCode()
        {
            int hash = 269;

            hash = (47 * hash) + Value.GetHashCode();
            hash = (47 * hash) + PolicyIdValue.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is AdminChildPolicyId)) return false;

            var childPolicyId = (AdminChildPolicyId)obj;

            return
                PolicyIdValue == childPolicyId.PolicyIdValue &&
                Value == childPolicyId.Value;
        }
    }
}
