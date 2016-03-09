using CQRSlite.Domain.Exception;
using Infrastructure.Exceptions;
using Security.BoundedContext.Domain.RootPolicy;
using Security.BoundedContext.Domain.RootPolicy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    public class UserRootPolicyAggregate : RootPolicyAggregate<UserRootPolicyAggregate>
    {
        public static readonly Guid NO_ACCESS_POLICY_ID = new Guid("1226688F-2546-47CA-9169-137F2A7CEAD2");

        protected UserRootPolicyAggregate()
            : base()
        {

        }

        protected UserRootPolicyAggregate(Guid id, string name, Guid? parentPolicyId)
            : base(id, name, parentPolicyId)
        {

        }

        public static UserRootPolicyAggregate Create(IPolicyService policyService, Guid? parentPolicyId, string policyName)
        {
            var aggregate = CheckRootPolicyCreationInvariantsAndCreateAggregate(
                NO_ACCESS_POLICY_ID,
                policyService,
                parentPolicyId,
                policyName,
                (newPolicyId) =>
                {
                    return new UserRootPolicyAggregate(newPolicyId, policyName, parentPolicyId);
                });

            return aggregate;
        }

    }
}
