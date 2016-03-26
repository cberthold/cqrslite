using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Identities.User
{
    public class AdminUserPolicyId
    {
        public Guid UserIdValue { get; private set; }
        public Guid Value { get; private set; }

        public AdminUserPolicyId(UserId userId, Guid id)
        {
            UserIdValue = userId.Value;
            Value = id;
        }

        public override int GetHashCode()
        {
            int hash = 269;

            hash = (47 * hash) + Value.GetHashCode();
            hash = (47 * hash) + UserIdValue.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is AdminUserPolicyId)) return false;

            var childPolicyId = (AdminUserPolicyId)obj;

            return
                UserIdValue == childPolicyId.UserIdValue &&
                Value == childPolicyId.Value;
        }
    }
}
