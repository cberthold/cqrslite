using Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Security.BoundedContext.Domain;
using Security.BoundedContext.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.BDDfy;

namespace Security.BoundedContext.Tests
{
    [TestClass]
    public class check_that_resource_actions_can_be_added_to_api_service
    {
        private ApiServicesAggregate aggregate;

        public Guid ApiGuid { get; set; }
        public string ApiName { get; set; }
        public string ResourceName { get; set; }
        public string ActionName { get; set; }

        // You can override step text using executable attributes
        void TheApiServicesAggregateIsNotCreated()
        {
            aggregate = null;
        }

        void TheApiIsCreated()
        {
            aggregate = ApiServicesAggregate.Create();
            Assert.AreEqual(aggregate.Id, ApiServicesAggregate.SERVICES_ID);
        }

        void TheApiServiceIsAdded()
        {
            aggregate.CreateService(ApiGuid);
            Assert.IsNotNull(aggregate.FindService(ApiGuid));
        }

        void ResourceActionIsAdded()
        {
            aggregate.CreateResourceAction(ApiGuid, ResourceName, ActionName);
        }

        void ResourceActionIsEnabled()
        {
            var service = aggregate.FindService(ApiGuid);
            var resourceAction = service.FindResourceAction(ResourceName, ActionName);

            aggregate.EnableResourceAction(ApiGuid, resourceAction.Id);
        }

        void TheAggregateIdShouldEqualApiGuid()
        {
            var service = aggregate.FindService(ApiGuid);
            Assert.AreEqual(service.Id, ApiGuid);
        }

        void TheAggregateNameShouldEqualApiName()
        {
            var service = aggregate.FindService(ApiGuid);
            Assert.AreEqual(service.Name, ApiName);
        }

        
        void AddingTheSameResourceActionShouldFailWithDomainError()
        {
            var duplicateFound = false;

            try
            {
                ResourceActionIsAdded();
            }
            catch (DomainException ex) when (ex.Message == "resource action already exists")
            {
                duplicateFound = true;
            }

            Assert.IsTrue(duplicateFound, "duplicate resource action not found");
        }

        [TestMethod]
        public void run_check_that_adding_api_service_to_list_works()
        {
            this.Given("I have <ApiGuid> to create service")
                .And(a => a.TheApiServicesAggregateIsNotCreated())
                .And(a => a.TheApiIsCreated())
                .And(a=> a.TheApiServiceIsAdded())
                .When(a => a.ResourceActionIsAdded())
                .And(a=> a.ResourceActionIsEnabled())
                .Then(a => a.TheAggregateIdShouldEqualApiGuid())
                .And(a => a.TheAggregateNameShouldEqualApiName())
                .And(a => a.AddingTheSameResourceActionShouldFailWithDomainError())
                .WithExamples(new ExampleTable("ApiGuid", "ApiName", "ResourceName", "ActionName")
                {
                    { ApiServiceEntity.CUSTOMER_API, ApiServiceEntity.CUSTOMER_API_NAME , "AddressController", "Create"},
                    { ApiServiceEntity.SECURITY_API, ApiServiceEntity.SECURITY_API_NAME , "RolesController", "Create"},
                    { ApiServiceEntity.SIGNALR_API, ApiServiceEntity.SIGNALR_API_NAME , "TestServerMethod", "Create"},
                })
                .BDDfy();
        }
    }
}
