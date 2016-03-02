using Autofac;
using CQRSlite.Domain;
using CQRSlite.Domain.Exception;
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
    public class check_that_adding_api_service_to_list_works : TestBase<check_that_adding_api_service_to_list_works>
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


        private ApiServiceAggregate aggregate;
        private IRepository repository;

        public Guid ApiGuid { get; set; }
        public string ApiName { get; set; }

        // You can override step text using executable attributes
        void TheApiServicesAggregateIsNotCreated()
        {
            aggregate = null;
        }
        
        void TheApiServiceIsAdded()
        {
            aggregate = ApiServiceAggregate.CreateService(ApiGuid);
            repository.Save(aggregate, aggregate.Version);
            Assert.IsNotNull(aggregate);
        }

        void TheAggregateIdShouldEqualApiGuid()
        {
            Assert.AreEqual(aggregate.Id, ApiGuid);
        }

        void TheAggregateNameShouldEqualApiName()
        {
            Assert.AreEqual(aggregate.Name, ApiName);
        }

        void TheAggregateShouldContainAnUncommittedEventForCreatingService()
        {
            var events = aggregate.GetUncommittedChanges();

            Assert.IsTrue(events.Count() == 2);
            Assert.IsTrue(events.Any(a => (a is ApiServiceCreated) && ((ApiServiceCreated)a).Id == ApiGuid));
        }

        void AddingTheSameAggregateIdShouldFailWithConcurrencyError()
        {
            var duplicateFound = false;

            try
            {
                aggregate = ApiServiceAggregate.CreateService(ApiGuid);
                repository.Save(aggregate, aggregate.Version);
                
            }
            catch (ConcurrencyException ex)
            {
                duplicateFound = true;
            }

            Assert.IsTrue(duplicateFound, "duplicate service not found");
        }

        [TestMethod]
        public void run_check_that_adding_api_service_to_list_works()
        {
            this.Given("I have <ApiGuid> to create service")
                .And(a => a.TheApiServicesAggregateIsNotCreated())
                .When(a => a.TheApiServiceIsAdded())
                .Then(a => a.TheAggregateIdShouldEqualApiGuid())
                .And(a => a.TheAggregateNameShouldEqualApiName())
                .And(a => a.AddingTheSameAggregateIdShouldFailWithConcurrencyError())
                .WithExamples(new ExampleTable("ApiGuid", "ApiName")
                {
                    { ApiServiceAggregate.CUSTOMER_API, ApiServiceAggregate.CUSTOMER_API_NAME },
                    { ApiServiceAggregate.SECURITY_API, ApiServiceAggregate.SECURITY_API_NAME },
                    { ApiServiceAggregate.SIGNALR_API, ApiServiceAggregate.SIGNALR_API_NAME },
                })
                .BDDfy();
        }
    }
}
