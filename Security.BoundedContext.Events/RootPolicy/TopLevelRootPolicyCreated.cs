using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events.RootPolicy
{
    public class TopLevelRootPolicyCreated : EventBase
    {
        public short PolicyType { get; protected set; }
        public string Name { get; protected set; }

        public TopLevelRootPolicyCreated(string name, short policyType)
        {
            Name = name;
            PolicyType = policyType;
        }
    }
}
