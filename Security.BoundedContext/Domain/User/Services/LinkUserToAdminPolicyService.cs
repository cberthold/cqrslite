using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.BoundedContext.Domain.User.Aggregate;
using Security.BoundedContext.Identities.RootPolicy;
using CQRSlite.Domain;
using Security.BoundedContext.Domain.RootPolicy.Aggregate;
using Security.BoundedContext.Identities.Feature;
using Security.BoundedContext.Identities.Common;

namespace Security.BoundedContext.Domain.User.Services
{
    public class LinkUserToAdminPolicyService :
        ILinkUserToAdminPolicyService
    {
        readonly IRepository repository;

        public LinkUserToAdminPolicyService(IRepository repository)
        {
            this.repository = repository;
        }

        public void LinkUserToAdminPolicy(UserAggregate user, AdminChildPolicyId childPolicy)
        {
            // link the user to the root policy 
            user.LinkUserToAdminRootPolicy(this, childPolicy);
           
            // save the user
            repository.Save(user);
        }

        public void UnlinkUserFromAdminPolicy(UserAggregate user, AdminChildPolicyId childPolicy)
        {
            // link the user to the root policy 
            user.UnlinkUserToAdminRootPolicy(this, childPolicy);

            // save the user
            repository.Save(user);
        }

        public void UpdateAdminUserPolicy(UserAggregate user, IDictionary<FeatureId, FeatureState> enabledFeatures)
        {
            var childPolicyId = user.AdminPolicy.AdminChildPolicyId;
            
            user.UpdateAdminUserPolicyPermissions(this, enabledFeatures);
        }

        public AdminRootPolicyAggregate LoadRootPolicy(AdminChildPolicyId childPolicyId)
        {
            var adminRootPolicy = repository.Get<AdminRootPolicyAggregate>(childPolicyId.PolicyIdValue);
            return adminRootPolicy;
        }

        public void SaveRootPolicy(AdminRootPolicyAggregate rootPolicy)
        {
            repository.Save(rootPolicy);
        }
        
    }
}
