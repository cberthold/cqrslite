using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Tests.Contracts.Commands;
using Infrastructure.Tests.Contracts.Events;
using Infrastructure.Exceptions;

namespace Infrastructure.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1 : TestBase
    {
        public UnitTest1() : base()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void WhenCreatingTheDomainObject_TheDomainObjectShouldBeCreatedWithTheRightName()
        {
            Guid id = Guid.NewGuid();
            When(new CreateDomainObject(id, "Chris B"));
            Then(new DomainObjectCreated(id, "Chris B"));
        }

        [TestMethod]
        public void GivenAUserWithIdXExists_WhenCreatingACustomerWithIdX_IShouldGetNotifiedThatTheUserAlreadyExists()
        {
            Guid id = Guid.NewGuid();
            Given(new DomainObjectCreated(id, "Some other guy"));
            WhenThrows<AggregateAlreadyExistsException<TestDomainObject>>(new CreateDomainObject(id, "Chris B"));
        }
    }
}
