using Infrastructure.Events;
using Security.BoundedContext.Identities.RootPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events.RootPolicy
{
    public class AdminRootPolicyInitialized : EventBase
    {
        public Guid PolicyId { get; private set; }
        public AdminRootPolicyInitialized(AdminRootPolicyId policyId)
        {
            PolicyId = policyId.Value;
        }
    }
}
