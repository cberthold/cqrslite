using Infrastructure.Events;
using Security.BoundedContext.Identities.Common;
using Security.BoundedContext.Identities.Feature;
using Security.BoundedContext.Identities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Events.User
{
    public class AdminUserPolicyPermissionsChanged : EventBase
    {
        public AdminUserPolicyId UserPolicyId { get; private set; }
        public IDictionary<FeatureId, FeatureState> Permissions { get; private set; }

        public AdminUserPolicyPermissionsChanged(AdminUserPolicyId userPolicyId, IDictionary<FeatureId, FeatureState> permissions)
        {
            UserPolicyId = userPolicyId;
            Permissions = permissions;
        }
    }
}
