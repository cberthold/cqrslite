using CQRSlite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.BoundedContext.Domain.Api.Aggregate;
using Security.BoundedContext.Domain.Api.Entities;
using Infrastructure.Exceptions;

namespace Security.BoundedContext.Domain.Api.Services
{
    public class ApiService : IApiService
    {
        private readonly IRepository repository;

        public ApiService(IRepository repository)
        {
            this.repository = repository;
        }

        public ResourceActionEntity CreateAndEnableResourceAction(Guid serviceId, string resourceName, string actionName)
        {
            var aggregate = LoadService(serviceId);

            if (aggregate == null)
                throw new DomainException($"Unable to find API service {serviceId}");

            var resourceAction = aggregate.CreateResourceAction(resourceName, actionName);
            aggregate.ActivateResourceAction(resourceAction.Id);
            repository.Save(aggregate, aggregate.Version);

            return resourceAction;
        }

        public ApiAggregate CreateService(Guid serviceId)
        {
            var aggregate = ApiAggregate.CreateService(serviceId);
            repository.Save(aggregate, aggregate.Version);
            return aggregate;
        }

        public ResourceActionEntity FindResourceAction(Guid serviceId, Guid resourceActionId)
        {
            var aggregate = LoadService(serviceId);

            if (aggregate == null)
                throw new DomainException($"Unable to find API service {serviceId}");

            return aggregate.FindResourceAction(resourceActionId);
        }

        public ResourceActionEntity FindResourceAction(Guid serviceId, string resourceName, string actionName)
        {
            var aggregate = LoadService(serviceId);

            if (aggregate == null)
                throw new DomainException($"Unable to find API service {serviceId}");

            return aggregate.FindResourceAction(resourceName, actionName);
        }

        public ApiAggregate LoadService(Guid serviceId)
        {
            var aggregate = repository.Get<ApiAggregate>(serviceId);
            return aggregate;
        }
    }
}
