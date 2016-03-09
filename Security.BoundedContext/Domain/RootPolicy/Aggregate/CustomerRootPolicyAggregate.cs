﻿using CQRSlite.Domain.Exception;
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
    public class CustomerRootPolicyAggregate : RootPolicyAggregate<CustomerRootPolicyAggregate>
    {

        public static readonly Guid NO_ACCESS_POLICY_ID = new Guid("A4E97CCC-F4AC-4781-A782-502287A5D33D");

        protected CustomerRootPolicyAggregate() 
            : base()
        {

        }

        protected CustomerRootPolicyAggregate(Guid id, string name, Guid? parentPolicyId)
            : base(id, name, parentPolicyId)
        {
            
        }
        
        public static CustomerRootPolicyAggregate Create(IPolicyService policyService, Guid? parentPolicyId, string policyName)
        {

            var aggregate = CheckRootPolicyCreationInvariantsAndCreateAggregate(
                NO_ACCESS_POLICY_ID,
                policyService,
                parentPolicyId,
                policyName,
                (newPolicyId) =>
                {
                    return new CustomerRootPolicyAggregate(newPolicyId, policyName, parentPolicyId);
                });
            
            return aggregate;
        }

    }
}
