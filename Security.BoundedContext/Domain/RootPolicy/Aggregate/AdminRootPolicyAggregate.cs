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
    public class AdminRootPolicyAggregate : RootPolicyAggregate<AdminRootPolicyAggregate>
    {
        
        public static readonly Guid NO_ACCESS_POLICY_ID = new Guid("0BE976F1-BDE2-414D-B7BE-F9F60D673A51");

        protected AdminRootPolicyAggregate()
            : base()
        {

        }

        protected AdminRootPolicyAggregate(Guid id, string name, Guid? parentPolicyId)
            : base(id, name, parentPolicyId)
        {

        }

        public static AdminRootPolicyAggregate Create(IPolicyService policyService, Guid? parentPolicyId, string policyName)
        {
            var aggregate = CheckRootPolicyCreationInvariantsAndCreateAggregate(
                NO_ACCESS_POLICY_ID,
                policyService,
                parentPolicyId,
                policyName,
                (newPolicyId) =>
                {
                    return new AdminRootPolicyAggregate(newPolicyId, policyName, parentPolicyId);
                });

            return aggregate;
        }

    }
}
