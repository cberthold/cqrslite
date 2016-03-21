using Infrastructure.Domain;
using Infrastructure.Exceptions;
using Security.BoundedContext.Identities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.Api.Entities
{
    public class ResourceActionEntity : IEntity
    {
        public Guid Id => ResourceActionId.Value;
        public ResourceActionId ResourceActionId { get; private set; }

        public string ResourceName { get; protected set; }
        public string ActionName { get; protected set; }
        public bool IsActive { get; protected set; }

        private ResourceActionEntity(ResourceActionId resourceActionId, string resourceName, string actionName)
        {
            ResourceActionId = resourceActionId;
            ResourceName = resourceName;
            ActionName = actionName;
        }

        public void SetResourceName(string resourceName)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
                throw new DomainException("resourceName is null or empty");

            this.ResourceName = resourceName;
        }

        public void SetActionName(string actionName)
        {
            if (string.IsNullOrWhiteSpace(actionName))
                throw new DomainException("actionName is null or empty");

            this.ActionName = actionName;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new DomainException($"Resource \"{ResourceName}\" with Action \"{ActionName}\" is already deactivated.");
            
            IsActive = false;
        }

        public static ResourceActionEntity Create(ResourceActionId resourceActionId, string resourceName, string actionName)
        {
            if(resourceActionId == null)
                throw new DomainException($"{nameof(resourceActionId)} is empty");
            if (string.IsNullOrWhiteSpace(resourceName))
                throw new DomainException($"{nameof(resourceName)} is null or empty");
            if (string.IsNullOrWhiteSpace(actionName))
                throw new DomainException($"{nameof(actionName)} is null or empty");

            return new ResourceActionEntity(resourceActionId, resourceName, actionName);
        }
    }
}
