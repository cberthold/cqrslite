using Infrastructure.Domain;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.Api.Entities
{
    public class ResourceActionEntity : IEntity
    {
        public Guid Id { get; protected set; }
        public Guid ApiServiceId { get; protected set; }
        public string ResourceName { get; protected set; }
        public string ActionName { get; protected set; }
        public bool IsActive { get; protected set; }

        private ResourceActionEntity(Guid entityId, Guid apiServiceId, string resourceName, string actionName)
        {
            Id = entityId;
            ApiServiceId = apiServiceId;
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

        public static ResourceActionEntity Create(Guid entityId, Guid apiServiceId, string resourceName, string actionName)
        {
            if (entityId == Guid.Empty)
                throw new DomainException("entityId is empty");
            if(apiServiceId == Guid.Empty)
                throw new DomainException("apiServiceId is empty");
            if (string.IsNullOrWhiteSpace(resourceName))
                throw new DomainException("resourceName is null or empty");
            if (string.IsNullOrWhiteSpace(actionName))
                throw new DomainException("actionName is null or empty");

            return new ResourceActionEntity(entityId, apiServiceId, resourceName, actionName);
        }
    }
}
