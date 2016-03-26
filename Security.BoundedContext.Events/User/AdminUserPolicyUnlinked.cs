using Infrastructure.Events;
using Security.BoundedContext.Identities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events.User
{
    public class AdminUserPolicyUnlinked : EventBase
    {
        public AdminUserPolicyId UserPolicyId { get; private set; }

        public AdminUserPolicyUnlinked(AdminUserPolicyId policyId)
        {
            UserPolicyId = policyId;
        }
    }
}
