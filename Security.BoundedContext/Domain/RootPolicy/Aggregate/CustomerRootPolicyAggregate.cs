using Security.BoundedContext.Domain.RootPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    public class CustomerRootPolicyAggregate : RootPolicyAggregate<CustomerRootPolicyAggregate>
    {

        public static readonly Guid NO_ACCESS_POLICY_ID = new Guid("A4E97CCC-F4AC-4781-A782-502287A5D33D");

        protected CustomerRootPolicyAggregate() 
            : base()
        {

        }
        


    }
}
