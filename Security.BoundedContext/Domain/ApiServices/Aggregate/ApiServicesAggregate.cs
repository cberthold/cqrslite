using CQRSlite.Domain;
using Infrastructure.Exceptions;
using Security.BoundedContext.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    public class ApiServicesAggregate : AggregateRoot
    {
        public static readonly Guid SERVICES_ID = new Guid(@"12E12F51-CDA4-4BB9-8BDF-606060F29E90");

        private IDictionary<Guid, ApiServiceEntity> _services;
        public ICollection<ApiServiceEntity> Services => _services.Values;

        private ApiServicesAggregate(Guid id) : this()
        {

            ApplyChange(new ApiServicesCreated(id));
        }

        public ApiServicesAggregate() : base()
        {
            _services = new Dictionary<Guid, ApiServiceEntity>();
        }

        public void Apply(ApiServicesCreated @event)
        {
            Id = @event.Id;
            Version = @event.Version;
        }

        public void Apply(ApiServiceEntityCreated @event)
        {
            var service = new ApiServiceEntity(@event.EntityId, @event.Name);
            _services[@event.EntityId] = service;
        }

        public void Apply(ResourceActionEntityCreated @event)
        {
            var service = FindService(@event.ApiServiceId);

            service.AddResourceAction(@event.ResourceName, @event.ActionName);
        }

        public void Apply(ResourceActionEntityDisabled @event)
        {
            var service = FindService(@event.ApiServiceId);

            var resourceAction = service.FindResourceAction(@event.EntityId);

            resourceAction.Disable();
        }

        public void Apply(ResourceActionEntityEnabled @event)
        {
            var service = FindService(@event.ApiServiceId);

            var resourceAction = service.FindResourceAction(@event.EntityId);

            resourceAction.Enable();
        }

        public void Apply(ResourceActionEntityRemoved @event)
        {
            var service = FindService(@event.ApiServiceId);

            var resourceAction = service.FindResourceAction(@event.EntityId);

            resourceAction.Enable();
        }

        public static ApiServicesAggregate Create()
        {
            return new ApiServicesAggregate(SERVICES_ID);
        }

        public ApiServiceEntity CreateService(Guid entityId)
        {
            if (_services.ContainsKey(entityId))
            {
                throw new DomainException("duplicate service");
            }

            if (entityId == ApiServiceEntity.SECURITY_API)
            {
                ApplyChange(new ApiServiceEntityCreated(entityId, ApiServiceEntity.SECURITY_API_NAME));
            }
            else if (entityId == ApiServiceEntity.CUSTOMER_API)
            {
                ApplyChange(new ApiServiceEntityCreated(entityId, ApiServiceEntity.CUSTOMER_API_NAME));
            }
            else if (entityId == ApiServiceEntity.SIGNALR_API)
            {
                ApplyChange(new ApiServiceEntityCreated(entityId, ApiServiceEntity.SIGNALR_API_NAME));
            }
            else
            {
                throw new DomainException("unknown service guid");
            }

            return FindService(entityId);
        }

        public ApiServiceEntity FindService(Guid id)
        {
            if (!_services.ContainsKey(id))
            {
                throw new DomainException("unable to find service");
            }

            var service = _services[id];

            return service;

        }

        public ResourceActionEntity CreateResourceAction(Guid apiServiceId, string resourceName, string actionName)
        {
            if (apiServiceId == Guid.Empty)
                throw new DomainException("apiServiceId cannot be empty");

            var service = FindService(apiServiceId);

            if(service.IsDuplicateResourceAction(resourceName, actionName))
                throw new DomainException("resource action already exists");

            var newId = Guid.NewGuid();

            ApplyChange(new ResourceActionEntityCreated(newId, apiServiceId, resourceName, actionName));

            return service.FindResourceAction(newId);

        }

        public void DisableResourceAction(Guid apiServiceId, Guid entityId)
        {
            if (apiServiceId == Guid.Empty)
                throw new DomainException("apiServiceId cannot be empty");

            var service = FindService(apiServiceId);

            var resourceAction = service.FindResourceAction(entityId);

            if (!resourceAction.IsEnabled)
                throw new DomainException($"Resource \"{resourceAction.ResourceName}\" with Action \"{resourceAction.ActionName}\" is already disabled.");

            ApplyChange(new ResourceActionEntityDisabled(apiServiceId, entityId));

        }

        public void EnableResourceAction(Guid apiServiceId, Guid entityId)
        {
            if (apiServiceId == Guid.Empty)
                throw new DomainException("apiServiceId cannot be empty");

            var service = FindService(apiServiceId);

            var resourceAction = service.FindResourceAction(entityId);

            if (resourceAction.IsEnabled)
                throw new DomainException($"Resource \"{resourceAction.ResourceName}\" with Action \"{resourceAction.ActionName}\" is already enabled.");

            ApplyChange(new ResourceActionEntityEnabled(apiServiceId, entityId));

        }
    }
}
