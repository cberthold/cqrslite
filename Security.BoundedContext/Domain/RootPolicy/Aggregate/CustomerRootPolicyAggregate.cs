using Security.BoundedContext.Domain.RootPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    public class CustomerRootPolicyAggregate : RootPolicyAggregate<CustomerIdentity>
    {
        protected CustomerRootPolicyAggregate() 
            : base()
        {

        }
    }
}
