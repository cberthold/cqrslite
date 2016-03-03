﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.RootPolicy
{
    public abstract class CompositePolicyBase : PolicyBase
    {

        protected List<PolicyBase> _childPolicies
            = new List<PolicyBase>();
        public IList<PolicyBase> ChildPolicies => _childPolicies.AsReadOnly();

        public PolicyBase ParentPolicy { get; protected set; }

        public CompositePolicyBase(Guid policyId) 
            : base(policyId)
        {
        }

        public virtual void SetParentPolicy(PolicyBase policy)
        {
            ParentPolicy = policy;

        }
    }
}
