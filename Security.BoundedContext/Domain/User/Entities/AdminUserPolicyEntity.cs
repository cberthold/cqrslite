using Infrastructure.Domain;
using Security.BoundedContext.Identities.Feature;
using Security.BoundedContext.Identities.RootPolicy;
using Security.BoundedContext.Identities.User;
using Security.BoundedContext.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.BoundedContext.Identities.Common;

namespace Security.BoundedContext.Domain.User.Entities
{
    public class AdminUserPolicyEntity : IEntity
    {
        #region Identity

        public Guid Id { get { return UserPolicyId.Value; } }
        public AdminUserPolicyId UserPolicyId { get; private set; }
        public AdminChildPolicyId AdminChildPolicyId { get; private set; }

        

        #endregion

        #region State

        public IDictionary<FeatureId, FeatureState> FeaturePermissions => featurePermissions.AsReadOnly();
        private IDictionary<FeatureId, FeatureState> featurePermissions;
        
        #endregion

        #region Constructors

        private AdminUserPolicyEntity()
        {
            featurePermissions = new Dictionary<FeatureId, FeatureState>();
        }

        private AdminUserPolicyEntity(AdminUserPolicyId policyId, AdminChildPolicyId childPolicyId)
            : this()
        {
            UserPolicyId = policyId;
            AdminChildPolicyId = childPolicyId;
        }

        #endregion

        #region Factory Methods

        public static AdminUserPolicyEntity Create(AdminUserPolicyId policyId, AdminChildPolicyId childPolicyId)
        {
            var entity = new AdminUserPolicyEntity(policyId, childPolicyId);

            return entity;
        }

        #endregion

        #region Domain Methods
        
        internal void UpdateFeaturePermissions(IDictionary<FeatureId, FeatureState> permissions)
        {
            featurePermissions = permissions;
        }
        #endregion
    }
}
