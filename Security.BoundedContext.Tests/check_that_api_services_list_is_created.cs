using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.BDDfy;
using Security.BoundedContext.Domain;

namespace Security.BoundedContext.Tests
{
    /// <summary>
    /// Summary description for EmptySystem
    /// </summary>
    [TestClass]
    public class check_that_api_services_list_is_created
    {
        private ApiServicesAggregate aggregate;

        // You can override step text using executable attributes
        void TheApiServicesAggregateIsNotCreated()
        {
            aggregate = null;
        }

        void TheApiIsCreated()
        {
            aggregate = ApiServicesAggregate.Create();
        }

        void TheAggregateIdShouldEqualApiGuid()
        {
            Assert.AreEqual(aggregate.Id, ApiServicesAggregate.SERVICES_ID);
        }
        
        
        [TestMethod]
        public void run_check_that_api_service_is_created_with_correct_name()
        {
            this.Given("I have <ApiGuid> to create service")
                .And(a=>a.TheApiServicesAggregateIsNotCreated())
                .When(a=> a.TheApiIsCreated())
                .Then(a=> a.TheAggregateIdShouldEqualApiGuid())
                .BDDfy();
        }
    }
}
