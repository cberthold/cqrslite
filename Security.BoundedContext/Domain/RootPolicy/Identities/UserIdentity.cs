using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.RootPolicy.Identities
{
    public class UserIdentity : Identity<Guid>
    {
        public UserIdentity(Guid userId, string name)
            : base(userId, name)
        {
        }
    }
}
