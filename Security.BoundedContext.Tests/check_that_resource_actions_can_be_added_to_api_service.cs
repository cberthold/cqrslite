using Autofac;
using CQRSlite.Domain;
using Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Security.BoundedContext.Domain;
using Security.BoundedContext.Domain.Api.Aggregate;
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
    public class check_that_resource_actions_can_be_added_to_api_service : TestBase<check_that_resource_actions_can_be_added_to_api_service>
    {

        [ClassInitialize]
        public static void ClassInit(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            ClassInitInternal(testContext);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            ClassCleanupInternal();
        }


        protected override void AfterTestInitialized()
        {
            repository = TestContainer.Resolve<IRepository>();
        }

        protected override void AfterTestCleanup()
        {
            repository = null;
        }


        private ApiAggregate aggregate;
        private IRepository repository;

        public Guid ApiGuid { get; set; }
        public string ApiName { get; set; }
        public string ResourceName { get; set; }
        public string ActionName { get; set; }

        // You can override step text using executable attributes
        void TheApiServicesAggregateIsNotCreated()
        {
            aggregate = null;
        }

        void TheApiServiceIsAdded()
        {
            aggregate = ApiAggregate.CreateService(ApiGuid);
            repository.Save(aggregate, aggregate.Version);
            Assert.IsNotNull(aggregate);
        }

        void ResourceActionIsAdded()
        {
            aggregate.CreateResourceAction(ResourceName, ActionName);
            repository.Save(aggregate, aggregate.Version);
        }

        void ResourceActionIsEnabled()
        {
            var resourceAction = aggregate.FindResourceAction(ResourceName, ActionName);

            aggregate.EnableResourceAction(resourceAction.Id);
            repository.Save(aggregate, aggregate.Version);

        }

        void TheAggregateIdShouldEqualApiGuid()
        {
            Assert.AreEqual(aggregate.Id, ApiGuid);
        }

        void TheAggregateNameShouldEqualApiName()
        {
            Assert.AreEqual(aggregate.Name, ApiName);
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
        public void run_check_that_resource_actions_can_be_added_to_api_service()
        {
            this.Given("I have <ApiGuid> to create service")
                .And(a => a.TheApiServicesAggregateIsNotCreated())
                //.And(a => a.TheApiIsCreated())
                .And(a=> a.TheApiServiceIsAdded())
                .When(a => a.ResourceActionIsAdded())
                .And(a=> a.ResourceActionIsEnabled())
                .Then(a => a.TheAggregateIdShouldEqualApiGuid())
                .And(a => a.TheAggregateNameShouldEqualApiName())
                .And(a => a.AddingTheSameResourceActionShouldFailWithDomainError())
                .WithExamples(new ExampleTable("ApiGuid", "ApiName", "ResourceName", "ActionName")
                {
                    { ApiAggregate.CUSTOMER_API, ApiAggregate.CUSTOMER_API_NAME , "AddressController", "Create"},
                    { ApiAggregate.SECURITY_API, ApiAggregate.SECURITY_API_NAME , "RolesController", "Create"},
                    { ApiAggregate.SIGNALR_API, ApiAggregate.SIGNALR_API_NAME , "TestServerMethod", "Create"},
                })
                .BDDfy();
        }
    }
}
