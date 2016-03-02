using CQRSlite.Domain;
using Infrastructure.Domain;
using Infrastructure.Exceptions;
using Security.BoundedContext.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    /// <summary>
    /// Entity represents the 
    /// </summary>
    public class ApiServiceAggregate : AggregateRoot
    {
        public static readonly Guid SECURITY_API = new Guid(@"4D04D504-71A5-4D2A-8A4E-16327F0D6BD7");
        public static readonly Guid CUSTOMER_API = new Guid(@"09B1B02B-9369-4AAB-A91A-0DFACC51F86F");
        public static readonly Guid SIGNALR_API = new Guid(@"138CC799-0FF5-42C1-A7FA-099ECA359F9E");

        public const string SECURITY_API_NAME = "Security API";
        public const string CUSTOMER_API_NAME = "Customer API";
        public const string SIGNALR_API_NAME = "SignalR API";

        public string Name { get; protected set; }

        IDictionary<Guid, ResourceActionEntity> _resourceActions;

        public ApiServiceAggregate() : base()
        {
            _resourceActions = new Dictionary<Guid, ResourceActionEntity>();
        }
        private ApiServiceAggregate(Guid id, string name) : this()
        {
            ApplyChange(new ApiServiceCreated(name));

            Id = id;
            Name = name;
        }

        public static ApiServiceAggregate CreateService(Guid id)
        {
            if (id == SECURITY_API)
            {
                return new ApiServiceAggregate(id, SECURITY_API_NAME);
            }
            else if (id == CUSTOMER_API)
            {
                return new ApiServiceAggregate(id, CUSTOMER_API_NAME);
            }
            else if (id == SIGNALR_API)
            {
                return new ApiServiceAggregate(id, SIGNALR_API_NAME);
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

        public ResourceActionEntity AddResourceAction(string resourceName, string actionName)
        {
            var newId = Guid.NewGuid();

            var resourceAction = ResourceActionEntity.Create(newId, Id, resourceName, actionName);

            _resourceActions[newId] = resourceAction;

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
            Id = @event.Id;
            Version = @event.Version;
            Name = @event.Name;
        }

        public void Apply(ResourceActionEntityCreated @event)
        {
            AddResourceAction(@event.ResourceName, @event.ActionName);
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
