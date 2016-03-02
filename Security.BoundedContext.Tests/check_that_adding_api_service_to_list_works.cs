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
    public class check_that_adding_api_service_to_list_works
    {
        private ApiServiceAggregate aggregate;

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

        //void AddingTheSameAggregateIdShouldFailWithDomainError()
        //{
        //    var duplicateFound = false;

        //    try
        //    {
        //        aggregate.CreateService(ApiGuid);
        //    }
        //    catch (DomainException ex) when (ex.Message == "duplicate service")
        //    {
        //        duplicateFound = true;
        //    }

        //    Assert.IsTrue(duplicateFound, "duplicate service not found");
        //}

        [TestMethod]
        public void run_check_that_adding_api_service_to_list_works()
        {
            this.Given("I have <ApiGuid> to create service")
                .And(a => a.TheApiServicesAggregateIsNotCreated())
                .When(a => a.TheApiServiceIsAdded())
                .Then(a => a.TheAggregateIdShouldEqualApiGuid())
                .And(a => a.TheAggregateNameShouldEqualApiName())
                //.And(a => a.AddingTheSameAggregateIdShouldFailWithDomainError())
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
