using CQRSlite.Domain;
using Infrastructure.Exceptions;
using Security.BoundedContext.Domain.RootPolicy.Aggregate;
using Security.BoundedContext.Domain.RootPolicy.Entities;
using Security.BoundedContext.Domain.RootPolicy.Services;
using Security.BoundedContext.Domain.User.Entities;
using Security.BoundedContext.Events.User;
using Security.BoundedContext.Identities.RootPolicy;
using Security.BoundedContext.Identities.User;
using Security.BoundedContext.ReadModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.BoundedContext.Identities.Feature;
using Security.BoundedContext.ValueObjects;
using Security.BoundedContext.Domain.User.Services;
using Security.BoundedContext.Identities.Common;

namespace Security.BoundedContext.Domain.User.Aggregate
{
    public class UserAggregate : AggregateRoot
    {

        #region Identities

        public override Guid Id
        {
            get { return UserId.Value; }
            protected set { UserId = new UserId(value); }
        }

        public UserId UserId { get; private set; }


        #endregion

        #region State

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string UserName { get; protected set; }

        public AdminUserPolicyEntity AdminPolicy { get; protected set; }

        #endregion

        #region Constructors

        private UserAggregate() {
        }

        protected UserAggregate(UserId userId, string userName, string firstName, string lastName)
            : this()
        {
            UserId = userId;
            ApplyChange(new UserProvisioned(UserId, userName, firstName, lastName));
        }


        #endregion

        #region Factory Methods

        public static UserAggregate Provision(string userName, string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new DomainException($"{nameof(userName)} can not be null or empty.");
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException($"{nameof(firstName)} can not be null or empty.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException($"{nameof(lastName)} can not be null or empty.");
            var id = new UserId(Guid.NewGuid());
            var aggregate = new UserAggregate(id, userName, firstName, lastName);

            return aggregate;   
        }

        #endregion

        #region Domain Methods

        internal void UnlinkUserToAdminRootPolicy(ILinkUserToAdminPolicyService service, AdminChildPolicyId linkedChildPolicy)
        {
            if (AdminPolicy != null) return;

            if (service == null)
                throw new DomainException($"{nameof(service)} cannot be null");
            if (linkedChildPolicy == null)
                throw new DomainException($"{nameof(linkedChildPolicy)} cannot be null");

            var rootPolicy = service.LoadRootPolicy(linkedChildPolicy);

            var childPolicy = rootPolicy.FindChildPolicy(linkedChildPolicy);

            var userPolicyId = AdminPolicy.UserPolicyId;

            // apply the state change
            ApplyChange(new AdminUserPolicyUnlinked(userPolicyId));

            // unlink the policy
            rootPolicy.UnlinkAdminChildPolicyFromUser(childPolicy, this);

            // save the root policy
            service.SaveRootPolicy(rootPolicy);

            // UserAggregate is going to be saved from the service
        }

        internal void LinkUserToAdminRootPolicy(ILinkUserToAdminPolicyService service, AdminChildPolicyId linkedChildPolicy)
        {
            if (AdminPolicy != null) return;

            if (service == null)
                throw new DomainException($"{nameof(service)} cannot be null");
            if (linkedChildPolicy == null)
                throw new DomainException($"{nameof(linkedChildPolicy)} cannot be null");

            var rootPolicy = service.LoadRootPolicy(linkedChildPolicy);

            var childPolicy = rootPolicy.FindChildPolicy(linkedChildPolicy);
            
            // create a new id to represent the admin policy linked to the child policy
            // being linked that will represent the final admin permission overrides
            var adminUserPolicyId = new AdminUserPolicyId(UserId, Guid.NewGuid());

            // apply the state change
            ApplyChange(new AdminUserPolicyLinked(adminUserPolicyId, linkedChildPolicy));

            rootPolicy.LinkAdminChildPolicyToUser(childPolicy, this);

            service.SaveRootPolicy(rootPolicy);

            // UserAggregate is going to be saved from the service
        }

        internal void UpdateAdminUserPolicyPermissions(ILinkUserToAdminPolicyService service, IDictionary<FeatureId, FeatureState> permissionUpdates)
        {
            if (service == null)
                throw new DomainException($"{nameof(service)} cannot be null");
            if (AdminPolicy == null)
                throw new DomainException($"Admin policy is not linked");

            var rootPolicy = service.LoadRootPolicy(AdminPolicy.AdminChildPolicyId);

            // get the available features from the root policy
            var availableFeatures = rootPolicy.AvailableFeatures;

            // set the playing field to inherit by default
            var finalPermissions = (from a in availableFeatures
                                             select new { Feature = a, State = FeatureState.Inherited })
                                             .ToDictionary((a) => a.Feature, (a) => a.State);

            // get the existing enablements from the user policy
            var existingPermissions = AdminPolicy.FeaturePermissions;

            // update the existing feature set ignoring any that have been removed
            foreach(var kvp in existingPermissions)
            {
                var key = kvp.Key;
                if(finalPermissions.ContainsKey(key))
                {
                    finalPermissions[key] = kvp.Value;
                }
            }

            // update with the feature permissions we're changing
            foreach(var kvp in permissionUpdates)
            {
                var key = kvp.Key;
                if(finalPermissions.ContainsKey(key))
                {
                    finalPermissions[key] = kvp.Value;
                }
            }

            // apply the state change
            ApplyChange(new AdminUserPolicyPermissionsChanged(AdminPolicy.UserPolicyId, finalPermissions));
        }
        #endregion

        #region Apply State

        public void Apply(UserProvisioned @event)
        {
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            UserName = @event.UserName;
        }

        public void Apply(AdminUserPolicyLinked @event)
        {
            AdminPolicy = AdminUserPolicyEntity.Create(@event.UserPolicyId, @event.AdminChildPolicyId);
        }

        public void Apply(AdminUserPolicyUnlinked @event)
        {
            if (AdminPolicy == null)
            {
                return;
            }
            else if (@event.UserPolicyId == AdminPolicy.UserPolicyId)
            {
                AdminPolicy = null;
            }
        }

        public void Apply(AdminUserPolicyPermissionsChanged @event)
        {
            if (AdminPolicy == null)
            {
                return;
            }

            AdminPolicy.UpdateFeaturePermissions(@event.Permissions);
        }
        #endregion
    }

    
}
