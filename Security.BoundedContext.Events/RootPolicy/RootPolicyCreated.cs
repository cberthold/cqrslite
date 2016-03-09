using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events.RootPolicy
{
    public class RootPolicyCreated : EventBase
    {
        public string Name { get; protected set; }
        public Guid? ParentPolicyId { get; protected set; }
        public RootPolicyCreated(string name, Guid? parentPolicyId)
        {
            Name = name;
            ParentPolicyId = parentPolicyId;
        }
    }
}
