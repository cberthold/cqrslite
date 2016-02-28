using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.RootPolicy
{
    public class NoAccessPolicy : CompositePolicyBase
    {
        public NoAccessPolicy(Guid policyId) 
            : base(policyId)
        {

        }
    }
}
