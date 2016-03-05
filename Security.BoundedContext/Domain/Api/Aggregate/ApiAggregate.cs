using CQRSlite.Domain;
using Infrastructure.Domain;
using Infrastructure.Exceptions;
using Security.BoundedContext.Domain.Api.Entities;
using Security.BoundedContext.Events;
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
        public static readonly Guid SECURITY_API = new Guid(@"4D04D504-71A5-4D2A-8A4E-16327F0D6BD7");
        public static readonly Guid CUSTOMER_API = new Guid(@"09B1B02B-9369-4AAB-A91A-0DFACC51F86F");
        public static readonly Guid SIGNALR_API = new Guid(@"138CC799-0FF5-42C1-A7FA-099ECA359F9E");

        public const string SECURITY_API_NAME = "Security API";
        public const string CUSTOMER_API_NAME = "Customer API";
        public const string SIGNALR_API_NAME = "SignalR API";

        public string Name { get; protected set; }

        IDictionary<Guid, ResourceActionEntity> _resourceActions;

        public ApiAggregate() : base()
        {
            _resourceActions = new Dictionary<Guid, ResourceActionEntity>();
        }
        private ApiAggregate(Guid id, string name) : this()
        {
            Id = id;
            ApplyChange(new ApiServiceCreated(name));
        }

        public static ApiAggregate CreateService(Guid id)
        {
            if (id == SECURITY_API)
            {
                return new ApiAggregate(id, SECURITY_API_NAME);
            }
            else if (id == CUSTOMER_API)
            {
                return new ApiAggregate(id, CUSTOMER_API_NAME);
            }
            else if (id == SIGNALR_API)
            {
                return new ApiAggregate(id, SIGNALR_API_NAME);
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

        public ResourceActionEntity AddResourceAction(Guid entityId, string resourceName, string actionName)
        {
            var resourceAction = ResourceActionEntity.Create(entityId, Id, resourceName, actionName);

            _resourceActions[entityId] = resourceAction;

            return resourceAction;
        }

        public ResourceActionEntity FindResourceAction(Guid entityId)
        {
            if (_resourceActions.ContainsKey(entityId))
                return _resourceActions[entityId];

            return null;
        }

        public void RenameResourceActionResourceName(Guid entityId, string resourceName)
        {

        }

        public bool IsDuplicateResourceAction(string resourceName, string actionName, Guid? entityIdToExclude = null)
        {
            if(entityIdToExclude == null)
            {
                return _resourceActions.Any(
                        a=>a.Value.ResourceName == resourceName
                        && a.Value.ActionName == actionName);
            }
            else
            {
                return _resourceActions.Any(a => 
                        a.Key !=  entityIdToExclude.Value
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

            var newId = Guid.NewGuid();

            ApplyChange(new ResourceActionEntityCreated(newId, resourceName, actionName));

            return FindResourceAction(newId);

        }

        public void DisableResourceAction(Guid entityId)
        {
            if (entityId == Guid.Empty)
                throw new DomainException("entityId cannot be empty");

            var resourceAction = FindResourceAction(entityId);

            if (!resourceAction.IsEnabled)
                throw new DomainException($"Resource \"{resourceAction.ResourceName}\" with Action \"{resourceAction.ActionName}\" is already disabled.");

            ApplyChange(new ResourceActionEntityDisabled(entityId));

        }

        public void EnableResourceAction(Guid entityId)
        {
            if (entityId == Guid.Empty)
                throw new DomainException("entityId cannot be empty");
            
            var resourceAction = FindResourceAction(entityId);

            if (resourceAction.IsEnabled)
                throw new DomainException($"Resource \"{resourceAction.ResourceName}\" with Action \"{resourceAction.ActionName}\" is already enabled.");

            ApplyChange(new ResourceActionEntityEnabled(entityId));

        }
        
        public void Apply(ApiServiceCreated @event)
        {
            Name = @event.Name;
        }

        public void Apply(ResourceActionEntityCreated @event)
        {
            AddResourceAction(@event.EntityId, @event.ResourceName, @event.ActionName);
        }

        public void Apply(ResourceActionEntityDisabled @event)
        {
            var resourceAction = FindResourceAction(@event.EntityId);

            resourceAction.Disable();
        }

        public void Apply(ResourceActionEntityEnabled @event)
        {
            var resourceAction = FindResourceAction(@event.EntityId);

            resourceAction.Enable();
        }

        public void Apply(ResourceActionEntityRemoved @event)
        {
            var resourceAction = FindResourceAction(@event.EntityId);

            resourceAction.Enable();
        }


    }
}
