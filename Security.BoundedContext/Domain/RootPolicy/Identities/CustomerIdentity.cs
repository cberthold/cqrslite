using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.RootPolicy
{
    public class CustomerIdentity : Identity<Guid>
    {
        public CustomerIdentity(Guid customerId, string name)
            : base(customerId, name)
        {
        }
    }
}
