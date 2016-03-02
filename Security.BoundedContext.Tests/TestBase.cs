using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Tests
{
    public abstract class TestBase<TTest>
        where TTest : TestBase<TTest>, new()
    {

        private static IContainer internalContainer;
        public ILifetimeScope TestContainer { get; private set; }

        
        public static void ClassInitInternal(TestContext testContext)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<TestModule>();

            // build IoC Container
            var container = builder.Build();

            internalContainer = container;
        }

        [TestInitialize]
        public void InitializeTest()
        {
            TestContainer = internalContainer.BeginLifetimeScope("AutofacWebRequest");
            AfterTestInitialized();
        }

        protected virtual void AfterTestInitialized()
        {

        }

        [TestCleanup]
        public void CleanupTest()
        {
            TestContainer.Dispose();
            TestContainer = null;
            AfterTestCleanup();
        }

        protected virtual void AfterTestCleanup()
        {

        }

        public static void ClassCleanupInternal()
        {   
            internalContainer.Dispose();
            internalContainer = null;
        }
    }
}
