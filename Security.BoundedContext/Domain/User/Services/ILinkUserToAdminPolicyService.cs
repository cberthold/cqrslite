using Security.BoundedContext.Domain.RootPolicy.Aggregate;
using Security.BoundedContext.Domain.User.Aggregate;
using Security.BoundedContext.Identities.RootPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.User.Services
{
    public interface ILinkUserToAdminPolicyService
    {
        void LinkUserToAdminPolicy(UserAggregate user, AdminChildPolicyId childPolicy);
        AdminRootPolicyAggregate LoadRootPolicy(AdminChildPolicyId childPolicyId);
        void SaveRootPolicy(AdminRootPolicyAggregate rootPolicy);
    }
}
