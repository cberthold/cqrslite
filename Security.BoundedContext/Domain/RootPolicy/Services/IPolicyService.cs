using Infrastructure.Domain;
using Security.BoundedContext.Domain.RootPolicy.Aggregate;
using Security.BoundedContext.Domain.RootPolicy.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.BoundedContext.Domain.User.Aggregate;
using Security.BoundedContext.ReadModel.DTO;

namespace Security.BoundedContext.Domain.RootPolicy.Services
{
    public interface IPolicyService : IDomainService
    {
        Guid GetRootPolicyId(PolicyTypes policyType);
    }
}
