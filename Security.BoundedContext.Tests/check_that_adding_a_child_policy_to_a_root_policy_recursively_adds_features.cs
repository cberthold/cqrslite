using Autofac;
using CQRSlite.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Security.BoundedContext.Domain;
using Security.BoundedContext.Domain.RootPolicy.Aggregate;
using Security.BoundedContext.Domain.RootPolicy.Identities;
using Security.BoundedContext.Domain.RootPolicy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.BDDfy;

namespace Security.BoundedContext.Tests
{
    [TestClass]
    public class check_that_adding_a_child_policy_to_a_root_policy_recursively_adds_features : TestBase<check_that_adding_a_child_policy_to_a_root_policy_recursively_adds_features>
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
            policyService = TestContainer.Resolve<IPolicyService>();
        }

        protected override void AfterTestCleanup()
        {
            repository = null;
            policyService = null;
        }

        private IRepository repository;
        private IPolicyService policyService;
        

        public PolicyTypes PolicyType { get; set; }
        public bool ResetContainer
        {
            set
            {
                if (value)
                {
                    CleanupTest();
                    InitializeTest();
                }
            }
        }

        public void TheRootPolicyIsCreated()
        {
            //aggregate = RootPolicyAggregate.Create(policyService, PolicyType);
            //repository.Save(aggregate, aggregate.Version);

        }

        public void ARootChildPolicyIsAddedToTheRootPolicy()
        {
            //string policyName = "Child Policy";
            //childAggregate = aggregate.AddRootChildPolicy(policyService, PolicyType, policyName);
        }

        public void AFeatureIsAddedToRootPolicy()
        {

        }

        public void TheFeatureExistsOnTheChildPolicy()
        {

        }

        [TestMethod]
        public void run_check_that_adding_a_child_policy_to_a_root_policy_recursively_adds_features_start_with_child_policy()
        {
            this.Given(a => a.TheRootPolicyIsCreated())
                .And(a => a.ARootChildPolicyIsAddedToTheRootPolicy())
                .When(a => a.AFeatureIsAddedToRootPolicy())
                .Then(a => a.TheFeatureExistsOnTheChildPolicy())
                .WithExamples(new ExampleTable("ResetContainer", "PolicyType")
                {
                    // run them first individually
                    { true, PolicyTypes.User },
                    { true, PolicyTypes.Customer },
                    { true, PolicyTypes.Admin },
                    // now run them together by not resetting the DI container
                    { true, PolicyTypes.User },
                    { false, PolicyTypes.Customer },
                    { false, PolicyTypes.Admin }
                })
                .BDDfy();
        }

        [TestMethod]
        public void run_check_that_adding_a_child_policy_to_a_root_policy_recursively_adds_features_start_with_feature_added()
        {
            this.Given(a => a.TheRootPolicyIsCreated())
                .And(a => a.AFeatureIsAddedToRootPolicy())
                .When(a => a.ARootChildPolicyIsAddedToTheRootPolicy())
                .Then(a => a.TheFeatureExistsOnTheChildPolicy())
                .WithExamples(new ExampleTable("ResetContainer", "PolicyType")
                {
                    // run them first individually
                    { true, PolicyTypes.User },
                    { true, PolicyTypes.Customer },
                    { true, PolicyTypes.Admin },
                    // now run them together by not resetting the DI container
                    { true, PolicyTypes.User },
                    { false, PolicyTypes.Customer },
                    { false, PolicyTypes.Admin }
                })
                .BDDfy();
        }
    }
}
