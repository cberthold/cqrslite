using CQRSlite.Domain;
using Security.BoundedContext.Domain.Feature.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.BoundedContext.Domain.RootPolicy.Aggregate;
using Security.BoundedContext.Domain.RootPolicy.Identities;
using Security.BoundedContext.Domain.User.Aggregate;
using Security.BoundedContext.ReadModel.DTO;

namespace Security.BoundedContext.Domain.RootPolicy.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IRepository repository;
        private readonly IFeatureService featureService;

        public PolicyService(IRepository repository, IFeatureService featureService)
        {
            this.repository = repository;
            this.featureService = featureService;
        }
        
        

        public Guid GetRootPolicyId(PolicyTypes policyType)
        {
            switch(policyType)
            {
                case PolicyTypes.Admin:
                    return Constants.ADMIN_POLICY;
                case PolicyTypes.Customer:
                    return Constants.CUSTOMER_POLICY;
                default:
                    throw new NotImplementedException();
            }
        }
        
        
    }
}
