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
using Security.BoundedContext.Domain.Api.Services;
using Security.BoundedContext.Identities.Feature;
using Security.BoundedContext.Domain.Feature.Entities;

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
            apiService = TestContainer.Resolve<IApiService>();
        }

        protected override void AfterTestCleanup()
        {
            repository = null;
        }


        private FeatureBookAggregate aggregate;
        private FeatureEntity feature;
        private IRepository repository;
        private IApiService apiService;

        public Guid ApiGuid { get; set; }
        public string ApiName { get; set; }
        public string ResourceName { get; set; }
        public string ActionName { get; set; }
        public string FeatureName { get; set; }
        public Guid FeatureBookId { get; set; }
        public FeatureId FeatureId { get; set; }


        // You can override step text using executable attributes
        void TheFeatureAggregateIsNotCreated()
        {
            aggregate = null;
        }

        void ResourceActionIsAddedToServiceApi()
        {
            var serviceAggregate = apiService.CreateService(ApiGuid);

            Assert.IsNotNull(serviceAggregate);
            Assert.IsFalse(serviceAggregate.Id == Guid.Empty);
            Assert.IsTrue(serviceAggregate.Id == ApiGuid);

            apiService.CreateAndEnableResourceAction(ApiGuid, ResourceName, ActionName);

        }


        void TheFeatureIsCreated()
        {
            aggregate = FeatureBookAggregate.Create();
            FeatureBookId = aggregate.Id;
            feature = aggregate.AddFeature(FeatureName);
            FeatureId = feature.FeatureId;

        }

        void TheResourceActionIsAddedToTheFeature()
        {
            var resourceAction = apiService.FindResourceAction(ApiGuid, ResourceName, ActionName);
            aggregate.AddResourceActionToFeature(feature, resourceAction);
        }

        void TheFeatureAggregateIsSavedToTheRepository()
        {
            repository.Save(aggregate);
        }

        void TheFeatureShouldBeAbleToBeLoadedFromRepository()
        {
            aggregate = repository.Get<FeatureBookAggregate>(FeatureBookId);
            Assert.IsNotNull(aggregate, "Feature Book couldnt be found in repository");
            var foundFeature = aggregate.GetFeature(FeatureId);
            Assert.IsNotNull(foundFeature, "Feature couldnt be found in feature book");
        }

        void TheFeatureAggregateNameShouldEqualToFeatureName()
        {
            Assert.AreEqual(feature.Name, FeatureName);
        }

        void TheLoadedResourceActionEntityIdsShouldBeFoundOnTheApiService()
        {
            Assert.IsTrue(feature.ResourceActions.Count > 0);

            foreach (var addedResourceAction in feature.ResourceActions)
            {
                var resourceAction = apiService.FindResourceAction(addedResourceAction);

                Assert.IsNotNull(resourceAction, $"Unable to find the resource action with Id {addedResourceAction.Value}");
                Assert.AreEqual(resourceAction.ResourceName, ResourceName);
                Assert.AreEqual(resourceAction.ActionName, ActionName);
                Assert.IsTrue(resourceAction.IsActive);

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
                    { Constants.CUSTOMER_API, Constants.CUSTOMER_API_NAME , "AddressController", "Create", "New Feature"},
                })
                .BDDfy();
        }
    }
}
