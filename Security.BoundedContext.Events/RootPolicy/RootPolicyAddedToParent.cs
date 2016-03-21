using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events.RootPolicy
{
    public class RootPolicyAddedToParent : EventBase
    {
        public Guid ParentPolicyId { get; protected set; }
        public Guid ChildEntityId { get; protected set; }
        public short PolicyType { get; protected set; }
        public string Name { get; protected set; }

        public RootPolicyAddedToParent(Guid childEntityId, string name, short policyType, Guid parentPolicyId)
        {
            ChildEntityId = childEntityId;
            Name = name;
            PolicyType = policyType;
            ParentPolicyId = parentPolicyId;
        }
    }
}
