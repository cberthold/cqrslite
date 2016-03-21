using Infrastructure.Domain;
using Security.BoundedContext.Domain.Api.Aggregate;
using Security.BoundedContext.Domain.Api.Entities;
using Security.BoundedContext.Identities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.Api.Services
{
    public interface IApiService : IDomainService
    {
        ApiAggregate CreateService(Guid serviceId);
        ApiAggregate LoadService(Guid serviceId);
        ResourceActionEntity CreateAndEnableResourceAction(Guid serviceId, string resourceName, string actionName);
        ResourceActionEntity FindResourceAction(Guid serviceId, string resourceName, string actionName);
        ResourceActionEntity FindResourceAction(ResourceActionId resourceActionId);
    }
}
