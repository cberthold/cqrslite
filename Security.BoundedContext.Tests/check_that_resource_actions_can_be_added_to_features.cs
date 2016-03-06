using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using CQRSlite.Domain;
using Security.BoundedContext.Domain;
using Infrastructure.Exceptions;
using TestStack.BDDfy;
using Security.BoundedContext.Domain.Feature;
using Security.BoundedContext.Domain.Feature.Aggregate;
using Security.BoundedContext.Domain.Api.Aggregate;

namespace Security.BoundedContext.Tests
{
    [TestClass]
    public class check_that_resource_actions_can_be_added_to_features : TestBase<check_that_resource_actions_can_be_added_to_features>
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


        private FeatureAggregate aggregate;
        private IRepository repository;

        public Guid ApiGuid { get; set; }
        public string ApiName { get; set; }
        public string ResourceName { get; set; }
        public string ActionName { get; set; }
        public string FeatureName { get; set; }
        public Guid FeatureId { get; set; }


        // You can override step text using executable attributes
        void TheFeatureAggregateIsNotCreated()
        {
            aggregate = null;
        }

        void ResourceActionIsAddedToServiceApi()
        {
            var serviceAggregate = ApiAggregate.CreateService(ApiGuid);
            repository.Save(serviceAggregate, serviceAggregate.Version);

            serviceAggregate = repository.Get<ApiAggregate>(ApiGuid);

            serviceAggregate.CreateResourceAction(ResourceName, ActionName);
            repository.Save(serviceAggregate, serviceAggregate.Version);

            serviceAggregate = repository.Get<ApiAggregate>(ApiGuid);

            var resourceAction = serviceAggregate.FindResourceAction(ResourceName, ActionName);

            serviceAggregate.EnableResourceAction(resourceAction.Id);
            repository.Save(serviceAggregate, serviceAggregate.Version);

            serviceAggregate = repository.Get<ApiAggregate>(ApiGuid);
        }


        void TheFeatureIsCreated()
        {
            aggregate = FeatureAggregate.Create(FeatureName);
            FeatureId = aggregate.Id;

        }

        void TheResourceActionIsAddedToTheFeature()
        {
            var serviceAggregate = repository.Get<ApiAggregate>(ApiGuid);
            var resourceAction = serviceAggregate.FindResourceAction(ResourceName, ActionName);
            aggregate.AddResourceActionToFeature(resourceAction);
        }

        void TheFeatureAggregateIsSavedToTheRepository()
        {
            repository.Save(aggregate);
        }

        void TheFeatureShouldBeAbleToBeLoadedFromRepository()
        {
            aggregate = repository.Get<FeatureAggregate>(FeatureId);
            Assert.IsNotNull(aggregate, "Feature couldnt be found in repository");
        }

        void TheFeatureAggregateNameShouldEqualToFeatureName()
        {
            Assert.AreEqual(aggregate.Name, FeatureName);
        }

        void TheLoadedResourceActionEntityIdsShouldBeFoundOnTheApiService()
        {
            Assert.IsTrue(aggregate.ResourceActions.Count > 0);

            foreach (var addedResourceAction in aggregate.ResourceActions)
            {
                var serviceId = addedResourceAction.ApiServiceId;
                var resourceActionId = addedResourceAction.ResourceActionEntityId;

                var serviceAggregate = repository.Get<ApiAggregate>(serviceId);

                Assert.IsNotNull(serviceAggregate, "Api service aggregate could not be found");

                var resourceAction = serviceAggregate.FindResourceAction(resourceActionId);

                Assert.IsNotNull(resourceAction, $"Unable to find the resource action with Id {resourceActionId}");
                Assert.AreEqual(resourceAction.ResourceName, ResourceName);
                Assert.AreEqual(resourceAction.ActionName, ActionName);

            }
        }

        [TestMethod]
        public void run_check_that_resource_actions_can_be_added_to_api_service()
        {
            this.Given("I have <ApiGuid> to create service")
                .And(a => a.TheFeatureAggregateIsNotCreated())
                //.And(a => a.TheApiIsCreated())
                .And(a => a.ResourceActionIsAddedToServiceApi())
                .When(a => a.TheFeatureIsCreated())
                .And(a => a.TheResourceActionIsAddedToTheFeature())
                .And(a => a.TheFeatureAggregateIsSavedToTheRepository())
                .And(a => a.TheFeatureShouldBeAbleToBeLoadedFromRepository())
                .And(a => a.TheFeatureAggregateNameShouldEqualToFeatureName())
                //.Then(a => a.TheFeatureShouldBeAbleToBeLoadedFromRepository())
                //.And(a => a.TheAggregateNameShouldEqualApiName())
                //.And(a => a.AddingTheSameResourceActionShouldFailWithDomainError())
                .WithExamples(new ExampleTable("ApiGuid", "ApiName", "ResourceName", "ActionName", "FeatureName")
                {
                    { ApiAggregate.CUSTOMER_API, ApiAggregate.CUSTOMER_API_NAME , "AddressController", "Create", "New Feature"},
                })
                .BDDfy();
        }
    }
}
