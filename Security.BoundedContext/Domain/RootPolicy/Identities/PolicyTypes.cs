using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.RootPolicy.Identities
{
    public enum PolicyTypes : short
    {
        Customer = 0,
        User = 1,
        Admin = 2
    }
}
