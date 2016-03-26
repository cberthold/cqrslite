using Infrastructure.Events;
using Security.BoundedContext.Identities.RootPolicy;
using Security.BoundedContext.Identities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events.User
{
    public class AdminUserPolicyLinked : EventBase
    {
        public AdminUserPolicyId UserPolicyId { get; private set; }
        public AdminChildPolicyId AdminChildPolicyId { get; private set; }
        public AdminUserPolicyLinked(AdminUserPolicyId policyId, AdminChildPolicyId rootPolicyId)
        {
            UserPolicyId = policyId;
            AdminChildPolicyId = rootPolicyId;
        }
    }
}
