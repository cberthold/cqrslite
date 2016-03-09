using Autofac;
using CQRSlite.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Security.BoundedContext.Domain;
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
                if(value)
                {
                    CleanupTest();
                    InitializeTest();
                }
            }
        }

        public void TheRootPolicyIsCreated()
        {
            switch(PolicyType)
            {
                case PolicyTypes.Admin:
                    var admaggregate = AdminRootPolicyAggregate.Create(policyService, null, "No access policy");
                    repository.Save(admaggregate);
                    break;
                case PolicyTypes.Customer:
                    var custaggregate = CustomerRootPolicyAggregate.Create(policyService, null, "No access policy");
                    repository.Save(custaggregate);
                    break;
                case PolicyTypes.User:
                    var useraggregate = UserRootPolicyAggregate.Create(policyService, null, "No access policy");
                    repository.Save(useraggregate);
                    break;
            }
            
        }

        public void AChildPolicyIsAddedToTheRootPolicy()
        {
            switch (PolicyType)
            {
                case PolicyTypes.Admin:
                    var admaggregate = AdminRootPolicyAggregate.Create(policyService, AdminRootPolicyAggregate.NO_ACCESS_POLICY_ID, "Administrators");
                    repository.Save(admaggregate);
                    break;
                case PolicyTypes.Customer:
                    var custaggregate = CustomerRootPolicyAggregate.Create(policyService, CustomerRootPolicyAggregate.NO_ACCESS_POLICY_ID, "Administrators");
                    repository.Save(custaggregate);
                    break;
                case PolicyTypes.User:
                    var useraggregate = UserRootPolicyAggregate.Create(policyService, UserRootPolicyAggregate.NO_ACCESS_POLICY_ID, "Administrators");
                    repository.Save(useraggregate);
                    break;
            }
            
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
                .And(a => a.AChildPolicyIsAddedToTheRootPolicy())
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
                .When(a => a.AChildPolicyIsAddedToTheRootPolicy())
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
