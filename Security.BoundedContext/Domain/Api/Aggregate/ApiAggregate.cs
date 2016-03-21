using CQRSlite.Domain;
using Infrastructure.Domain;
using Infrastructure.Exceptions;
using Security.BoundedContext.Domain.Api.Entities;
using Security.BoundedContext.Events;
using Security.BoundedContext.Identities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.Api.Aggregate
{
    /// <summary>
    /// Entity represents the 
    /// </summary>
    public class ApiAggregate : AggregateRoot
    {
        #region Identity
        public override Guid Id
        {
            get { return ApiId.Value; }
            protected set { ApiId = new ApiId(value); }
        }

        public ApiId ApiId { get; protected set; }

        #endregion

        #region State

        public string Name { get; protected set; }

        IDictionary<ResourceActionId, ResourceActionEntity> _resourceActions;

        #endregion

        public ApiAggregate() : base()
        {
            _resourceActions = new Dictionary<ResourceActionId, ResourceActionEntity>();
        }
        private ApiAggregate(ApiId apiId, string name) : this()
        {
            ApplyChange(new ApiServiceCreated(apiId, name));
        }

        public static ApiAggregate CreateService(Guid id)
        {
            var apiId = new ApiId(id);
            if (id == Constants.SECURITY_API)
            {
                return new ApiAggregate(apiId, Constants.SECURITY_API_NAME);
            }
            else if (id == Constants.CUSTOMER_API)
            {
                return new ApiAggregate(apiId, Constants.CUSTOMER_API_NAME);
            }
            else if (id == Constants.SIGNALR_API)
            {
                return new ApiAggregate(apiId, Constants.SIGNALR_API_NAME);
            }
            else
            {
                throw new DomainException("unknown service guid");
            }
        }

        public ResourceActionEntity FindResourceAction(string resourceName, string actionName)
        {
            var resourceAction = _resourceActions.Values.FirstOrDefault(a => a.ResourceName == resourceName && a.ActionName == actionName);

            return resourceAction;
        }

        public ResourceActionEntity AddResourceAction(ResourceActionId id, string resourceName, string actionName)
        {
            var resourceAction = ResourceActionEntity.Create(id, resourceName, actionName);

            _resourceActions[id] = resourceAction;

            return resourceAction;
        }

        public ResourceActionEntity FindResourceAction(ResourceActionId entityId)
        {
            if (_resourceActions.ContainsKey(entityId))
                return _resourceActions[entityId];

            return null;
        }

        public bool IsDuplicateResourceAction(string resourceName, string actionName, ResourceActionId entityIdToExclude = null)
        {
            if (entityIdToExclude == null)
            {
                return _resourceActions.Any(
                        a => a.Value.ResourceName == resourceName
                        && a.Value.ActionName == actionName);
            }
            else
            {
                return _resourceActions.Any(a =>
                        a.Key != entityIdToExclude
                        && a.Value.ResourceName == resourceName
                        && a.Value.ActionName == actionName);
            }

        }

        public ResourceActionEntity CreateResourceAction(string resourceName, string actionName)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
                throw new DomainException("resourceName cannot be empty");
            if (string.IsNullOrWhiteSpace(actionName))
                throw new DomainException("actionName cannot be empty");

            if (IsDuplicateResourceAction(resourceName, actionName))
                throw new DomainException("resource action already exists");

            var newId = new ResourceActionId(ApiId, Guid.NewGuid());

            ApplyChange(new ResourceActionEntityCreated(newId, resourceName, actionName));

            return FindResourceAction(newId);

        }

        public void DeactivateResourceAction(ResourceActionId entityId)
        {
            if (entityId == null)
                throw new DomainException("entityId cannot be null");

            var resourceAction = FindResourceAction(entityId);

            if (!resourceAction.IsActive)
                throw new DomainException($"Resource \"{resourceAction.ResourceName}\" with Action \"{resourceAction.ActionName}\" is already disabled.");

            ApplyChange(new ResourceActionEntityDeactivated(entityId));

        }

        public void ActivateResourceAction(ResourceActionEntity resourceAction)
        {
            if (resourceAction == null)
                throw new DomainException("resourceAction cannot be null");
            ActivateResourceAction(resourceAction.ResourceActionId);
        }
        public void ActivateResourceAction(ResourceActionId entityId)
        {
            if (entityId == null)
                throw new DomainException("entityId cannot be null");

            var resourceAction = FindResourceAction(entityId);

            if (resourceAction.IsActive)
                throw new DomainException($"Resource \"{resourceAction.ResourceName}\" with Action \"{resourceAction.ActionName}\" is already enabled.");

            ApplyChange(new ResourceActionEntityActivated(entityId));

        }

        public void RemoveResourceAction(ResourceActionId entityId)
        {
            if (entityId == null)
                throw new DomainException("entityId cannot be null");

            var resourceAction = FindResourceAction(entityId);

            if (resourceAction == null) return;

            ApplyChange(new ResourceActionEntityRemoved(entityId));

        }

        public void RenameResourceActionResourceName(ResourceActionId entityId, string resourceName)
        {
            if (entityId == null)
                throw new DomainException("entityId cannot be null");
            if (string.IsNullOrWhiteSpace(resourceName))
                throw new DomainException($"{nameof(resourceName)} cannot be null or empty");

            var resourceAction = FindResourceAction(entityId);

            if (resourceAction.ResourceName == resourceName) return;

            ApplyChange(new ResourceActionEntityResourceNameChanged(entityId, resourceName));
        }

        public void RenameResourceActionActionName(ResourceActionId entityId, string actionName)
        {
            if (entityId == null)
                throw new DomainException("entityId cannot be empty");
            if (string.IsNullOrWhiteSpace(actionName))
                throw new DomainException($"{nameof(actionName)} cannot be null or empty");

            var resourceAction = FindResourceAction(entityId);

            if (resourceAction.ResourceName == actionName) return;

            ApplyChange(new ResourceActionEntityActionNameChanged(entityId, actionName));
        }

        public void Apply(ApiServiceCreated @event)
        {
            ApiId = @event.ApiId;
            Name = @event.Name;
        }

        public void Apply(ResourceActionEntityCreated @event)
        {
            AddResourceAction(@event.EntityId, @event.ResourceName, @event.ActionName);
        }

        public void Apply(ResourceActionEntityDeactivated @event)
        {
            var resourceAction = FindResourceAction(@event.EntityId);

            resourceAction.Deactivate();
        }

        public void Apply(ResourceActionEntityActivated @event)
        {
            var resourceAction = FindResourceAction(@event.EntityId);

            resourceAction.Activate();
        }

        public void Apply(ResourceActionEntityRemoved @event)
        {
            var resourceAction = FindResourceAction(@event.EntityId);

            resourceAction.Activate();
        }

        public void Apply(ResourceActionEntityActionNameChanged @event)
        {
            var resourceAction = FindResourceAction(@event.EntityId);

            resourceAction.SetActionName(@event.ActionName);
        }

        public void Apply(ResourceActionEntityResourceNameChanged @event)
        {
            var resourceAction = FindResourceAction(@event.EntityId);

            resourceAction.SetResourceName(@event.ResourceName);
        }


    }
}
