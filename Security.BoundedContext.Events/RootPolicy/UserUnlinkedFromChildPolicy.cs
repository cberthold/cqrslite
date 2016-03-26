using Infrastructure.Events;
using Security.BoundedContext.Identities.RootPolicy;
using Security.BoundedContext.Identities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events.RootPolicy
{
    public class UserUnlinkedFromChildPolicy : EventBase
    {
        public AdminUserPolicyId UserPolicyId { get; private set; }
        public AdminChildPolicyId AdminChildPolicyId { get; private set; }
        public UserUnlinkedFromChildPolicy(AdminChildPolicyId childPolicyId, AdminUserPolicyId userPolicyId)
        {
            UserPolicyId = userPolicyId;
            AdminChildPolicyId = childPolicyId;
        }
    }
}
