using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.RootPolicy
{
    public class CustomerUserMappedPolicy : UserMappedPolicy<CustomerIdentity>
    {
        protected CustomerUserMappedPolicy(Guid policyId, CustomerIdentity mappedIdentity)
            : base(policyId, mappedIdentity)
        {

        }

        public static CustomerUserMappedPolicy Create(Guid policyId, string customerName, Guid customerId)
        {
            var cutomerIdentity = new CustomerIdentity(customerId, customerName);
            var policy = new CustomerUserMappedPolicy(policyId, cutomerIdentity);

            return policy;
        }
    }
}
