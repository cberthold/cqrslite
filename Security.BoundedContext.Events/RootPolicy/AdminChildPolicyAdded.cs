using Infrastructure.Events;
using Security.BoundedContext.Identities.RootPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events.RootPolicy
{
    public class AdminChildPolicyAdded : EventBase
    {
        public AdminChildPolicyId PolicyId { get; private set; }
        public string Name { get; private set; }

        public AdminChildPolicyAdded(AdminChildPolicyId policyId, string policyName)
        {
            PolicyId = policyId;
            Name = policyName;
        }
    }
}
